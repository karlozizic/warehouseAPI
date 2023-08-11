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
    
    public async Task<List<ItemRequestEntity>> GetItemRequests()
    {
        return await _itemRequestRepository.GetItemRequests();
    }
    
    public async Task<ItemRequestEntity?> GetItemRequestById(Guid id)
    {
        if (!await _itemRequestRepository.Exists(id))
        {
            throw new Exception("Item request does not exist");
        }
        
        return await _itemRequestRepository.GetItemRequestById(id);
    }

    public async Task CreateItemRequest(ItemRequestDto itemRequestDto, Guid operatorId)
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

        await _itemRequestRepository.InsertItemRequest(itemRequestEntity); 
    }

    public async Task UpdateItemRequest(Guid itemRequestId, ItemRequestEnum itemRequestStatus, Guid operatorId)
    {
        ItemRequestEntity itemRequestEntity = await _itemRequestRepository.GetItemRequestById(itemRequestId);
        
        if (itemRequestEntity == null)
        {
            throw new Exception("Item request does not exist");
        }
        
        if (itemRequestEntity.CurrentWarehouseId != null)
        {
            ItemEntity itemEntity = await _itemRepository.GetItemById(itemRequestEntity.ItemId);

            if (itemEntity == null)
            {
                throw new Exception("Item does not exist");
            }
        }
        
        FranchiseUserEntity franchiseUserEntity = await _franchiseUserRepository.GetFranchiseUserById(operatorId);
        
        if (franchiseUserEntity == null)
        {
            throw new Exception("Franchise user does not exist");
        }
        
        if (itemRequestEntity.Status == ItemRequestEnum.Requested)
        {
            if (franchiseUserEntity.WarehouseId == itemRequestEntity.CurrentWarehouseId)
            {
                itemRequestEntity.Status = itemRequestStatus;
                await _itemRequestRepository.UpdateItemRequest(itemRequestEntity);
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
                await _itemRequestRepository.UpdateItemRequest(itemRequestEntity);
                if (itemRequestEntity.Status == ItemRequestEnum.ApprovedForEntering)
                {
                    // ako item vec postoji u nekom warehouse
                    if (itemRequestEntity.CurrentWarehouseId != null)
                    {
                        ItemEntity itemEntity = await _itemRepository.GetItemById(itemRequestEntity.ItemId);
                        if (itemEntity == null)
                        {
                            throw new Exception("Item does not exist");
                        }
                        itemEntity.WarehouseId = itemRequestEntity.RequestedWarehouseId;
                        await _itemRepository.UpdateItem(itemEntity);
                    } else
                    {
                      ItemEntity newItemEntity = new ItemEntity(itemRequestEntity.ItemId, itemRequestEntity.ItemName,
                          itemRequestEntity.ItemDescription, itemRequestEntity.RequestedWarehouseId);
                      await _itemRepository.InsertItem(newItemEntity); 
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
    
    public async Task DeleteItemRequest(Guid id)
    {
        if (!await _itemRequestRepository.Exists(id))       
        {
            throw new Exception("Item request does not exist");
        }
        
        await _itemRequestRepository.DeleteItemRequest(id);
    }
}

public interface IItemRequestService
{
    public Task<List<ItemRequestEntity>> GetItemRequests();
    public Task<ItemRequestEntity> GetItemRequestById(Guid id);
    public Task CreateItemRequest(ItemRequestDto itemRequestEntity, Guid operatorId);
    public Task UpdateItemRequest(Guid id, ItemRequestEnum itemRequestDto, Guid operatorId);
    public Task DeleteItemRequest(Guid id);
}