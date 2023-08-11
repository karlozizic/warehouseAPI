using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    
    public ItemService(IItemRepository itemRepository, IWarehouseRepository warehouseRepository)
    {
        _itemRepository = itemRepository;
        _warehouseRepository = warehouseRepository;
    }
    
    public async Task UpdateItem(Guid tenantId, ItemEntity warehouseItemEntity)
    {
        if (!await _itemRepository.ExistsItem(tenantId, warehouseItemEntity.Id))
        {
            throw new Exception("Item does not exist");
        }

        await _itemRepository.UpdateItem(tenantId, warehouseItemEntity);
    }
    
    public async Task<ItemEntity> GetItemById(Guid tenantId, Guid id)
    {
        if (!await _itemRepository.ExistsItem(tenantId, id))
        {
            throw new Exception("Item does not exist");
        }
        
        return await _itemRepository.GetItemById(tenantId, id);
    }
    
    public async Task<List<ItemEntity>> GetWarehouseItems(Guid tenantId, Guid warehouseId, String? name)
    {
        if (!await _warehouseRepository.Exists(tenantId, warehouseId))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        List<ItemEntity> items = await _itemRepository.GetWarehouseItems(tenantId, warehouseId);
        
        // filtriranje bi bilo bolje napraviti odmah u queryju
        if (name != null)
        {
            List<ItemEntity> filteredItems = items.Where(item => item.Name == name).ToList();
            return filteredItems;
        }

        return items; 
    }

}

public interface IItemService
{
    Task UpdateItem(Guid tenantId, ItemEntity warehouseItemEntity);
    Task<ItemEntity> GetItemById(Guid tenantId, Guid id);
    Task<List<ItemEntity>> GetWarehouseItems(Guid tenantId, Guid warehouseId, String? name);
}