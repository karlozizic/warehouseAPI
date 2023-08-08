using Microsoft.EntityFrameworkCore;
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
    
    public async Task<bool> Exists(Guid userId)
    {
        var franchiseUser = await _warehouseContext.FranchiseUser.FirstOrDefaultAsync(x => x.UserId == userId);
        
        if (franchiseUser == null)
        {
            return false;
        }

        return true; 
    }
    
    public async Task<FranchiseUserEntity> GetFranchiseUserById(Guid userId)
    {
        var franchiseUser = await _warehouseContext.FranchiseUser.FirstOrDefaultAsync(x => x.UserId == userId);
        
        if (franchiseUser == null)
        {
            throw new Exception("Franchise user does not exist");
        }

        return franchiseUser; 
    }
}