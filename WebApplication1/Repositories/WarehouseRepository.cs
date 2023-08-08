using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class WarehouseRepository : IWarehouseRepository, IDisposable
{
    private WarehouseContext _warehouseContext;
    
    public WarehouseRepository(WarehouseContext warehouseContext)
    {
        _warehouseContext = warehouseContext;
        //ContextService
    }
    
    public async Task<List<WarehouseEntity>> GetWarehouses()
    {
        //await using (var context = contextService.CreateDbContext){ <tu se izvodi kod> }  
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

    public async Task UpdateWarehouse(WarehouseUpdateClass warehouseUpdate)
    {
        // sljedeci nacin nije radio
        /*_warehouseContext.Attach(warehouseEntity); 
        _warehouseContext.Entry(warehouseEntity).State = EntityState.Modified;
        await _warehouseContext.SaveChangesAsync();*/
        
        //provjera je li warehouse deleted 
        
        Guid id = warehouseUpdate.Id; 
        WarehouseEntity? warehouseEntity = await _warehouseContext.Warehouse.SingleAsync(x => x.Id == id);

        if (!warehouseUpdate.Name.IsNullOrEmpty())
        {
            warehouseEntity.Name = warehouseUpdate.Name;
        }
        if(warehouseUpdate.Location != null)
        {
            warehouseEntity.Location = warehouseUpdate.Location;
        }
        if (!warehouseUpdate.PhoneNumber.IsNullOrEmpty())
        {
            warehouseEntity.PhoneNumber = warehouseUpdate.PhoneNumber;
        }
        if (!warehouseUpdate.Code.IsNullOrEmpty())
        {
          warehouseEntity.Code = warehouseUpdate.Code;  
        }
        // nije moguce deleteati preko update requesta 
        if (!warehouseUpdate.DefaultLanguage.IsNullOrEmpty())
        {
            warehouseEntity.DefaultLanguage = warehouseUpdate.DefaultLanguage;
        }
        // sto ako je warehouseUpdate.IsPayoutLockedForOtherCostCenter == null? 
        if (warehouseUpdate.IsPayoutLockedForOtherCostCenter)
        {
            warehouseEntity.IsPayoutLockedForOtherCostCenter = true;
        }
        if (warehouseUpdate.OperatorUser != null)
        {
            warehouseEntity.OperatorUser = warehouseUpdate.OperatorUser;
        }
        // warehouse items update? 
        if (warehouseUpdate.Items != null)
        {
            warehouseEntity.Items = warehouseUpdate.Items;
        }
        
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

    public async Task InsertAllWarehouses(List<WarehouseEntity> warehouseEntities)
    {
        await _warehouseContext.Warehouse.AddRangeAsync(warehouseEntities);
        await _warehouseContext.SaveChangesAsync(); 
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