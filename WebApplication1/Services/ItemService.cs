using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    
    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    
    public async Task UpdateItem(Item warehouseItem)
    {
        if (!await _itemRepository.ExistsItem(warehouseItem.Id))
        {
            throw new Exception("Item does not exist");
        }

        await _itemRepository.UpdateItem(warehouseItem);
    }

}