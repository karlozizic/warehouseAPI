using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;

namespace WebApplication1.Repositories;

public class ItemRequestRepository : IItemRequestRepository
{
    private WarehouseContext _warehouseContext;
    
    public ItemRequestRepository(WarehouseContext warehouseContext)
    {
        _warehouseContext = warehouseContext;
    }
    
    public async Task<List<ItemRequestEntity>> GetItemRequests()
    {
        return await _warehouseContext.ItemRequest.ToListAsync(); 
    }
    
    public async Task<ItemRequestEntity?> GetItemRequestById(Guid id)
    {
        return await _warehouseContext.ItemRequest.FindAsync(id); 
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _warehouseContext.ItemRequest.AnyAsync(e => e.Id == id);
    }
    
    public async Task InsertItemRequest(ItemRequestEntity itemRequestEntity)
    {
        await Task.FromResult(_warehouseContext.ItemRequest.Add(itemRequestEntity)); 
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task DeleteItemRequest(Guid id)
    {
        ItemRequestEntity? itemRequest = await _warehouseContext.ItemRequest.FindAsync(id);
        itemRequest.Deleted = true;
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task UpdateItemRequest(ItemRequestEntity itemRequestEntity)
    {
        _warehouseContext.Entry(itemRequestEntity).State = EntityState.Modified;
        await _warehouseContext.SaveChangesAsync();
    }
    
}

public interface IItemRequestRepository
{
    public Task<List<ItemRequestEntity>> GetItemRequests();
    public Task<ItemRequestEntity?> GetItemRequestById(Guid id);
    public Task<bool> Exists(Guid id);
    public Task InsertItemRequest(ItemRequestEntity itemRequestEntity);
    public Task DeleteItemRequest(Guid id);
    public Task UpdateItemRequest(ItemRequestEntity itemRequestEntity);
}