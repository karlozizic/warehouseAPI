using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Services;

namespace WebApplication1.Repositories;

public class ItemRequestRepository : IItemRequestRepository
{
    private readonly IContextService _contextService;
    
    public ItemRequestRepository(IContextService contextService)
    {
        _contextService = contextService;
    }
    
    public async Task<List<ItemRequestEntity>> GetItemRequests(Guid tenantId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.ItemRequest.ToListAsync(); 
        }
    }
    
    public async Task<ItemRequestEntity?> GetItemRequestById(Guid tenantId, Guid id)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.ItemRequest.FindAsync(id); 
        }
    }

    public async Task<bool> Exists(Guid tenantId, Guid id)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.ItemRequest.AnyAsync(e => e.Id == id);
        }
    }

    public async Task InsertItemRequest(Guid tenantId, ItemRequestEntity itemRequestEntity)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            await Task.FromResult(warehouseContext.ItemRequest.Add(itemRequestEntity));
            await warehouseContext.SaveChangesAsync();
        }
    }

    public async Task DeleteItemRequest(Guid tenantId, Guid id)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            ItemRequestEntity? itemRequest = await warehouseContext.ItemRequest.FindAsync(id);
            itemRequest.Deleted = true;
            await warehouseContext.SaveChangesAsync();
        }
    }
    
    public async Task UpdateItemRequest(Guid tenantId, ItemRequestEntity itemRequestEntity)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            warehouseContext.Entry(itemRequestEntity).State = EntityState.Modified;
            await warehouseContext.SaveChangesAsync();
        }
    }
    
}

public interface IItemRequestRepository
{
    public Task<List<ItemRequestEntity>> GetItemRequests(Guid tenantId);
    public Task<ItemRequestEntity?> GetItemRequestById(Guid tenantId, Guid id);
    public Task<bool> Exists(Guid tenantId, Guid id);
    public Task InsertItemRequest(Guid tenantId, ItemRequestEntity itemRequestEntity);
    public Task DeleteItemRequest(Guid tenantId, Guid id);
    public Task UpdateItemRequest(Guid tenantId, ItemRequestEntity itemRequestEntity);
}