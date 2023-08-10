using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly WarehouseContext _warehouseContext;
    
    public ItemRepository(WarehouseContext warehouseContext)
    {
        _warehouseContext = warehouseContext;
    }
    
    public async Task UpdateItem(ItemEntity warehouseItemEntity)
    {
        _warehouseContext.Entry(warehouseItemEntity).State = EntityState.Modified;
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task<Boolean> ExistsItem(Guid id)
    {
        return await _warehouseContext.Item.AnyAsync(e => e.Id == id);
    }

    public async Task<ItemEntity> GetItemById(Guid id)
    {
        return await _warehouseContext.Item.FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public async Task InsertItem(ItemEntity itemEntity)
    {
        _warehouseContext.Item.Add(itemEntity);
        await _warehouseContext.SaveChangesAsync();
    }

    public async Task DeleteItem(Guid id)
    {
        _warehouseContext.Item.Remove(await _warehouseContext.Item.FirstOrDefaultAsync(e => e.Id == id));
        await _warehouseContext.SaveChangesAsync();
    }
}
