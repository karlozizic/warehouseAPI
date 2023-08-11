using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly IContextService _contextService;
    
    public ItemRepository(IContextService contextService)
    {
        _contextService = contextService;
    }
    
    public async Task<List<ItemEntity>> GetItems(Guid tenantId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.Item.ToListAsync();
        }
    }

    public async Task<List<ItemEntity>> GetWarehouseItems(Guid tenantId, Guid warehouseId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.Item
                .Where(x => x.WarehouseId == warehouseId)
                .ToListAsync();
        }
        
    }
    
    public async Task<ItemEntity> GetItemById(Guid tenantId, Guid id)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.Item.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
    
    public async Task<Boolean> ExistsItem(Guid tenantId, Guid id)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.Item.AnyAsync(e => e.Id == id);
        }
    }
    
    public async Task UpdateItem(Guid tenantId, ItemEntity warehouseItemEntity)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            warehouseContext.Entry(warehouseItemEntity).State = EntityState.Modified;
            await warehouseContext.SaveChangesAsync();
        }
        
    }
    
    public async Task InsertItem(Guid tenantId, ItemEntity itemEntity)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            warehouseContext.Item.Add(itemEntity);
            await warehouseContext.SaveChangesAsync();
        }
    }

    public async Task DeleteItem(Guid tenantId, Guid id)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            warehouseContext.Item.Remove(await warehouseContext.Item.FirstOrDefaultAsync(e => e.Id == id));
            await warehouseContext.SaveChangesAsync();
        }
    }
}

public interface IItemRepository
{
    Task<List<ItemEntity>> GetItems(Guid tenantId);
    Task<List<ItemEntity>> GetWarehouseItems(Guid tenantId, Guid warehouseId);
    Task UpdateItem(Guid tenantId, ItemEntity warehouseItemEntity);
    Task<Boolean> ExistsItem(Guid tenantId, Guid id);
    Task<ItemEntity> GetItemById(Guid tenantId, Guid id);
    Task InsertItem(Guid tenantId, ItemEntity itemEntity);
    Task DeleteItem(Guid tenantId, Guid id);
}