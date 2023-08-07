using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories;

public class WarehouseRepository : IWarehouseRepository, IDisposable
{
    private WarehouseContext _warehouseContext;
    
    public WarehouseRepository(WarehouseContext warehouseContext)
    {
        _warehouseContext = warehouseContext;
    }
    
    public async Task<List<WarehouseEntity>> GetWarehouses()
    {
        //ToListAsync vraca Task<List<Warehouse>> - nije potrebno Task.FromResult(...)
        return await _warehouseContext.Warehouse.ToListAsync(); 
    }

    // ** Provjeri jos! - fetch po Guid id 
    public async Task<WarehouseEntity?> GetWarehouseById(Guid id)
    {
        // Find i FirstOrDefault - oboje imaju slozenost O(n) 
        // Find 
        return await _warehouseContext.Warehouse.FindAsync(id); 
        // FirstOrDefault iterira sa foreach loopom kroz sve elemente kolekcije i vraca prvi element koji zadovoljava uvjet
        // return await _warehouseContext.Warehouses.FirstOrDefaultAsync(x => x.id == id);
    }

    public async Task<Boolean> Exists(Guid id)
    {
        return await _warehouseContext.Warehouse.AnyAsync(x => x.Id == id);
    }
    
    // ** Sljedeci blok metoda vraca void, ali asinkrone su pa se vraca Task 
    public async Task InsertWarehouse(WarehouseEntity warehouseEntity)
    {
        await Task.FromResult(_warehouseContext.Warehouse.Add(warehouseEntity)); 
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task DeleteWarehouse(Guid id)
    {
        WarehouseEntity? warehouse = await _warehouseContext.Warehouse.FindAsync(id);
        // Radimo soft delete - ne brisemo iz baze nego samo postavljamo deleted na true
        //_warehouseContext.Warehouses.Remove(warehouse);
        warehouse.Deleted = true;
        await _warehouseContext.SaveChangesAsync();
    }

    public async Task UpdateWarehouse(WarehouseEntity warehouseEntity)
    {
        _warehouseContext.Entry(warehouseEntity).State = EntityState.Modified;
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task AssignOperator(FranchiseUserEntity franchiseUserEntity, Guid warehouseId)
    {
        WarehouseEntity? warehouse = await _warehouseContext.Warehouse.FindAsync(warehouseId);
        warehouse.OperatorUser = franchiseUserEntity;
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId)
    {
        
        return await _warehouseContext.Warehouse
            .Where(x => x.Id == warehouseId)
            .SelectMany(x => x.Items)
            .ToListAsync();
    }
    
    public async Task<FranchiseUserEntity> GetOperator(Guid warehouseId)
    {
        WarehouseEntity? warehouse = await _warehouseContext.Warehouse.FindAsync(warehouseId);
        return warehouse.OperatorUser;
    }

    // https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    // Implementacija IDisposable interface-a 
    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _warehouseContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
}