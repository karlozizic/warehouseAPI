using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories;

public class FranchiseUserRepository : IFranchiseUserRepository
{
    public WarehouseContext _warehouseContext;
    
    public FranchiseUserRepository(WarehouseContext warehouseContext)
    {
        _warehouseContext = warehouseContext;
    }
    
    public async Task InsertAllFranchiseUsers(List<FranchiseUserEntity> franchiseUserEntities)
    {
        await _warehouseContext.FranchiseUser.AddRangeAsync(franchiseUserEntities);
        await _warehouseContext.SaveChangesAsync(); 
    }
}