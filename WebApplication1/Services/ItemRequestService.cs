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
        ItemRequestEntity itemRequestEntity = new ItemRequestEntity(itemRequestDto.itemId, itemRequestDto.warehouseId, 
            ItemRequestEnum.Requested, operatorId);

        await _itemRequestRepository.InsertItemRequest(itemRequestEntity); 
    }

    public async Task UpdateItemRequest(Guid id, ItemRequestEnum itemRequestStatus, Guid operatorId)
    {
        ItemRequestEntity itemRequestEntity = await _itemRequestRepository.GetItemRequestById(id);
        
        if (itemRequestEntity == null)
        {
            throw new Exception("Item request does not exist");
        }
        
        ItemEntity itemEntity = await _itemRepository.GetItemById(itemRequestEntity.itemId);
        
        if (itemEntity == null)
        {
            throw new Exception("Item does not exist");
        }
        
        FranchiseUserEntity franchiseUserEntity = await _franchiseUserRepository.GetFranchiseUserById(operatorId);
        
        if (franchiseUserEntity == null)
        {
            throw new Exception("Franchise user does not exist");
        }
        
        // provjera je li warehouseId unutar FranchiseUsera 
        if (itemEntity.WarehouseId != franchiseUserEntity.WarehouseId)
        {
            throw new Exception("Francise user does not have access to this warehouse");
        }
        
        itemRequestEntity.status = itemRequestStatus; 
        await _itemRequestRepository.UpdateItemRequest(itemRequestEntity);

        if (itemRequestStatus == ItemRequestEnum.Approved)
        {
            // item insert u warehouse
            await _itemRepository.InsertItem(itemEntity);
        }
        
    }
}

public interface IItemRequestService
{
    public Task<List<ItemRequestEntity>> GetItemRequests();
    public Task<ItemRequestEntity> GetItemRequestById(Guid id);
    public Task CreateItemRequest(ItemRequestDto itemRequestEntity, Guid operatorId);
    
    public Task UpdateItemRequest(Guid id, ItemRequestEnum itemRequestDto, Guid operatorId);

}