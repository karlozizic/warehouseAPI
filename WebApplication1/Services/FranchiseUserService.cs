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
    
    public async Task InsertFranchiseUsers(List<FranchiseUserDto> franchiseUsers)
    {
        List<FranchiseUserEntity> franchiseUserEntities = new List<FranchiseUserEntity>();

        foreach (var franchiseUser in franchiseUsers)
        {
            franchiseUserEntities.Add(new FranchiseUserEntity(franchiseUser.Id,
                franchiseUser.UserId, franchiseUser.FranchiseId, franchiseUser.Username, franchiseUser.TenantId, null));
        }
        
        await _franchiseUserRepository.InsertAllFranchiseUsers(franchiseUserEntities);
    }
    
    public async Task AssignToWarehouse(Guid franchiseUserId, Guid warehouseId)
    {
        
        FranchiseUserUpdateClass franchiseUserUpdate = new FranchiseUserUpdateClass();
        franchiseUserUpdate.WarehouseId = warehouseId;
        
        await _franchiseUserRepository.UpdateFranchiseUser(franchiseUserUpdate, franchiseUserId);
    }

    public async Task<FranchiseUserEntity> GetFranchiseUserById(Guid franchiseUserId)
    {
        if (!await _franchiseUserRepository.Exists(franchiseUserId))
        {
            throw new Exception("Franchise User doesn't exist");
        }
        
        return await _franchiseUserRepository.GetFranchiseUserById(franchiseUserId);
    }
}