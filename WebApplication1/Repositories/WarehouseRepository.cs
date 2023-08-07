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
    
    public async Task<List<Warehouse>> GetWarehouses()
    {
        //ToListAsync vraca Task<List<Warehouse>> - nije potrebno Task.FromResult(...)
        return await _warehouseContext.Warehouses.ToListAsync(); 
    }

    // ** Provjeri jos! - fetch po Guid id 
    public async Task<Warehouse?> GetWarehouseById(Guid id)
    {
        // Find i FirstOrDefault - oboje imaju slozenost O(n) 
        // Find 
        return await _warehouseContext.Warehouses.FindAsync(id); 
        // FirstOrDefault iterira sa foreach loopom kroz sve elemente kolekcije i vraca prvi element koji zadovoljava uvjet
        // return await _warehouseContext.Warehouses.FirstOrDefaultAsync(x => x.id == id);
    }

    public async Task<Boolean> Exists(Guid id)
    {
        return await _warehouseContext.Warehouses.AnyAsync(x => x.Id == id);
    }
    
    // ** Sljedeci blok metoda vraca void, ali asinkrone su pa se vraca Task 
    public async Task InsertWarehouse(Warehouse warehouse)
    {
        await Task.FromResult(_warehouseContext.Warehouses.Add(warehouse)); 
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task DeleteWarehouse(Guid id)
    {
        Warehouse? warehouse = await _warehouseContext.Warehouses.FindAsync(id);
        // Radimo soft delete - ne brisemo iz baze nego samo postavljamo deleted na true
        //_warehouseContext.Warehouses.Remove(warehouse);
        warehouse.Deleted = true;
        await _warehouseContext.SaveChangesAsync();
    }

    public async Task UpdateWarehouse(Warehouse warehouse)
    {
        _warehouseContext.Entry(warehouse).State = EntityState.Modified;
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task AssignOperator(FranchiseUser franchiseUser, Guid warehouseId)
    {
        Warehouse? warehouse = await _warehouseContext.Warehouses.FindAsync(warehouseId);
        warehouse.OperatorUser = franchiseUser;
        await _warehouseContext.SaveChangesAsync();
    }
    
    public async Task<List<Item>> GetWarehouseItems(Guid warehouseId)
    {
        
        return await _warehouseContext.Warehouses
            .Where(x => x.Id == warehouseId)
            .SelectMany(x => x.Items)
            .ToListAsync();
    }
    
    public async Task<FranchiseUser> GetOperator(Guid warehouseId)
    {
        Warehouse? warehouse = await _warehouseContext.Warehouses.FindAsync(warehouseId);
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