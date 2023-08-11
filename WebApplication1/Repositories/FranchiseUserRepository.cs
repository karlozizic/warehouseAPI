using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Repositories;

public class FranchiseUserRepository : IFranchiseUserRepository
{
    private readonly IContextService _contextService;
    
    public FranchiseUserRepository(IContextService contextService)
    {
        _contextService = contextService;
    }
    
    public async Task InsertAllFranchiseUsers(Guid tenantId, List<FranchiseUserEntity> franchiseUserEntities)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            await warehouseContext.FranchiseUser.AddRangeAsync(franchiseUserEntities);
            await warehouseContext.SaveChangesAsync();
        }
    }
    
    public async Task<bool> Exists(Guid tenantId, Guid userId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            var franchiseUser = await warehouseContext.FranchiseUser.FirstOrDefaultAsync(x => x.UserId == userId);

            if (franchiseUser == null)
            {
                return false;
            }

            return true;
        }
    }
    
    public async Task<FranchiseUserEntity> GetFranchiseUserById(Guid tenantId, Guid userId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            var franchiseUser = await warehouseContext.FranchiseUser.FirstOrDefaultAsync(x => x.UserId == userId);

            if (franchiseUser == null)
            {
                throw new Exception("Franchise user does not exist");
            }

            return franchiseUser;
        }
    }
    
    public async Task UpdateFranchiseUser(Guid tenantId, FranchiseUserUpdateClass franchiseUserUpdateClass, Guid franchiseUserId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            var franchiseUser =
                await warehouseContext.FranchiseUser.FirstOrDefaultAsync(x => x.UserId == franchiseUserId);

            if (franchiseUser == null)
            {
                throw new Exception("Franchise user does not exist");
            }

            if (franchiseUserUpdateClass.WarehouseId != null)
            {
                franchiseUser.WarehouseId = franchiseUserUpdateClass.WarehouseId;
            }

            if (franchiseUserUpdateClass.Username != null)
            {
                franchiseUser.Username = franchiseUserUpdateClass.Username;
            }

            await warehouseContext.SaveChangesAsync();
        }
    }
}