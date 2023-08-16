using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IFranchiseUserRepository _franchiseUserRepository;
    
    public ItemService(IItemRepository itemRepository, 
        IWarehouseRepository warehouseRepository, IFranchiseUserRepository franchiseUserRepository)
    {
        _itemRepository = itemRepository;
        _warehouseRepository = warehouseRepository;
        _franchiseUserRepository = franchiseUserRepository;
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
    
    public async Task ReserveItem(Guid tenantId, Guid userId, Guid itemId)
    {
        
        ItemEntity? item = await _itemRepository.GetItemById(tenantId, itemId);
        if (item == null)
        {
            throw new Exception("Item does not exist");
        }
        
        //provjera je li User Operator warehouse-a u kojem je Item
        FranchiseUserEntity? user = await _franchiseUserRepository.GetFranchiseUserById(tenantId, userId);
        if (user == null)
        {
            throw new Exception("User does not exist"); 
        }

        if (user.Id != userId)
        {
            throw new Exception("User is not operator of this warehouse"); 
        }

        item.reserved = true; 
        await _itemRepository.UpdateItem(tenantId, item);
    }

}

public interface IItemService
{
    Task UpdateItem(Guid tenantId, ItemEntity warehouseItemEntity);
    Task<ItemEntity> GetItemById(Guid tenantId, Guid id);
    Task<List<ItemEntity>> GetWarehouseItems(Guid tenantId, Guid warehouseId, String? name);
    Task ReserveItem(Guid tenantId, Guid userId, Guid itemId); 
}