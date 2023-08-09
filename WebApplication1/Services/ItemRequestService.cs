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
    
    public ItemRequestService(IItemRequestRepository itemRequestRepository, IItemRepository itemRepository)
    {
        _itemRequestRepository = itemRequestRepository;
        _itemRepository = itemRepository;
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

    public async Task CreateItemRequest(ItemRequestDto itemRequestDto)
    {
        ItemRequestEntity itemRequestEntity = new ItemRequestEntity(itemRequestDto.itemId, itemRequestDto.warehouseId, 
            ItemRequestEnum.Requested, itemRequestDto.operatorId);

        await _itemRequestRepository.InsertItemRequest(itemRequestEntity); 
    }
}

public interface IItemRequestService
{
    public Task<List<ItemRequestEntity>> GetItemRequests();
    public Task<ItemRequestEntity> GetItemRequestById(Guid id);
    public Task CreateItemRequest(ItemRequestDto itemRequestEntity);
}