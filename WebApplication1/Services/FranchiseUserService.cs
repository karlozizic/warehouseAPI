using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Services;

public class FranchiseUserService : IFranchiseUserService
{
    private readonly IFranchiseUserRepository _franchiseUserRepository;
    
    public FranchiseUserService(IFranchiseUserRepository franchiseUserRepository)
    {
        _franchiseUserRepository = franchiseUserRepository;
    }
    
    public async Task InsertFranchiseUsers(Guid tenantId, List<FranchiseUserDto> franchiseUsers)
    {
        List<FranchiseUserEntity> franchiseUserEntities = new List<FranchiseUserEntity>();

        foreach (var franchiseUser in franchiseUsers)
        {
            franchiseUserEntities.Add(new FranchiseUserEntity(franchiseUser.Id,
                franchiseUser.UserId, franchiseUser.FranchiseId, franchiseUser.Username, franchiseUser.TenantId));
        }
        
        await _franchiseUserRepository.InsertAllFranchiseUsers(tenantId, franchiseUserEntities);
    }
    
    public async Task AssignToWarehouse(Guid tenantId, Guid franchiseUserId, Guid warehouseId)
    {
        
        FranchiseUserUpdateClass franchiseUserUpdate = new FranchiseUserUpdateClass();
        franchiseUserUpdate.WarehouseId = warehouseId;
        
        await _franchiseUserRepository.UpdateFranchiseUser(tenantId, franchiseUserUpdate, franchiseUserId);
    }

    public async Task<FranchiseUserEntity> GetFranchiseUserById(Guid tenantId, Guid franchiseUserId)
    {
        if (!await _franchiseUserRepository.Exists(tenantId, franchiseUserId))
        {
            throw new Exception("Franchise User doesn't exist");
        }
        
        return await _franchiseUserRepository.GetFranchiseUserById(tenantId, franchiseUserId);
    }
}