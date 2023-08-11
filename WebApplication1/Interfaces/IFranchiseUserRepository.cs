using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserRepository
{
    public Task InsertAllFranchiseUsers(Guid tenantId, List<FranchiseUserEntity> franchiseUserEntities);

    public Task<bool> Exists(Guid tenantId, Guid userId);
    
    public Task<FranchiseUserEntity> GetFranchiseUserById(Guid tenantId, Guid userId);

    public Task UpdateFranchiseUser(Guid tenantId, FranchiseUserUpdateClass franchiseUserUpdateClass, Guid franchiseUserId); 
}