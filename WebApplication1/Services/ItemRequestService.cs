using System.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database.Entities;
using WebApplication1.Enums;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ItemRequestService : IItemRequestService
{
    private readonly IItemRequestRepository _itemRequestRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IFranchiseUserRepository _franchiseUserRepository;
    
    public ItemRequestService(IItemRequestRepository itemRequestRepository, IItemRepository itemRepository,
        IFranchiseUserRepository franchiseUserRepository)
    {
        _itemRequestRepository = itemRequestRepository;
        _itemRepository = itemRepository;
        _franchiseUserRepository = franchiseUserRepository;
    }
    
    public async Task<List<ItemRequestEntity>> GetItemRequests(Guid tenantId)
    {
        return await _itemRequestRepository.GetItemRequests(tenantId);
    }
    
    public async Task<ItemRequestEntity?> GetItemRequestById(Guid tenantId, Guid id)
    {
        if (!await _itemRequestRepository.Exists(tenantId, id))
        {
            throw new Exception("Item request does not exist");
        }
        
        return await _itemRequestRepository.GetItemRequestById(tenantId, id);
    }

    public async Task CreateItemRequest(Guid tenantId, ItemRequestDto itemRequestDto, Guid operatorId)
    {
        ItemRequestEntity itemRequestEntity = new ItemRequestEntity(itemRequestDto.ItemId, itemRequestDto.ItemName,
            itemRequestDto.ItemDescription, operatorId, itemRequestDto.CurrentWarehouseId, itemRequestDto.RequestedWarehouseId);
        
        // item nije trenutno u drugom warehouseu
        if (itemRequestDto.CurrentWarehouseId == null)
        {
            itemRequestEntity.Status = ItemRequestEnum.ApprovedForLeaving;
        }
        else
        {
            itemRequestEntity.Status = ItemRequestEnum.Requested;
        }

        await _itemRequestRepository.InsertItemRequest(tenantId, itemRequestEntity); 
    }

    public async Task UpdateItemRequest(Guid tenantId, Guid itemRequestId, ItemRequestEnum itemRequestStatus, Guid operatorId)
    {
        ItemRequestEntity itemRequestEntity = await _itemRequestRepository.GetItemRequestById(tenantId, itemRequestId);
        
        if (itemRequestEntity == null)
        {
            throw new Exception("Item request does not exist");
        }
        
        if (itemRequestEntity.CurrentWarehouseId != null)
        {
            ItemEntity itemEntity = await _itemRepository.GetItemById(tenantId, itemRequestEntity.ItemId);

            if (itemEntity == null)
            {
                throw new Exception("Item does not exist");
            }

            if (itemEntity.reserved == true)
            {
                throw new Exception("Item is reserved by warehouse operator - can't be moved");
            }
        }

        FranchiseUserEntity franchiseUserEntity = await _franchiseUserRepository.GetFranchiseUserById(tenantId, operatorId);
        
        if (franchiseUserEntity == null)
        {
            throw new Exception("Franchise user does not exist");
        }
        
        if (itemRequestEntity.Status == ItemRequestEnum.Requested)
        {
            if (franchiseUserEntity.WarehouseId == itemRequestEntity.CurrentWarehouseId)
            {
                itemRequestEntity.Status = itemRequestStatus;
                await _itemRequestRepository.UpdateItemRequest(tenantId, itemRequestEntity);
            }
            else
            {
                throw new Exception("Operator is not in the same warehouse as the item");
            }
            
        } else if(itemRequestEntity.Status == ItemRequestEnum.ApprovedForLeaving)
        {
            if (franchiseUserEntity.WarehouseId == itemRequestEntity.RequestedWarehouseId)
            {
                itemRequestEntity.Status = itemRequestStatus;
                await _itemRequestRepository.UpdateItemRequest(tenantId, itemRequestEntity);
                if (itemRequestEntity.Status == ItemRequestEnum.ApprovedForEntering)
                {
                    // ako item vec postoji u nekom warehouse
                    if (itemRequestEntity.CurrentWarehouseId != null)
                    {
                        ItemEntity itemEntity = await _itemRepository.GetItemById(tenantId, itemRequestEntity.ItemId);
                        if (itemEntity == null)
                        {
                            throw new Exception("Item does not exist");
                        }
                        itemEntity.WarehouseId = itemRequestEntity.RequestedWarehouseId;
                        await _itemRepository.UpdateItem(tenantId, itemEntity);
                    } else
                    {
                      ItemEntity newItemEntity = new ItemEntity(itemRequestEntity.ItemId, itemRequestEntity.ItemName,
                          itemRequestEntity.ItemDescription, itemRequestEntity.RequestedWarehouseId);
                      await _itemRepository.InsertItem(tenantId, newItemEntity); 
                    }
                }
            }
            else
            {
                throw new Exception("Operator is not in the same warehouse as the item"); 
            }
        } 
        else
        {
            throw new Exception("Invalid Request or Item is not approved for leaving warehouse"); 
        }
    }
    
    public async Task DeleteItemRequest(Guid tenantId, Guid id)
    {
        if (!await _itemRequestRepository.Exists(tenantId, id))       
        {
            throw new Exception("Item request does not exist");
        }
        
        await _itemRequestRepository.DeleteItemRequest(tenantId, id);
    }
}

public interface IItemRequestService
{
    public Task<List<ItemRequestEntity>> GetItemRequests(Guid tenantId);
    public Task<ItemRequestEntity> GetItemRequestById(Guid tenantId, Guid id);
    public Task CreateItemRequest(Guid tenantId, ItemRequestDto itemRequestEntity, Guid operatorId);
    public Task UpdateItemRequest(Guid tenantId, Guid id, ItemRequestEnum itemRequestDto, Guid operatorId);
    public Task DeleteItemRequest(Guid tenantId, Guid id);
}