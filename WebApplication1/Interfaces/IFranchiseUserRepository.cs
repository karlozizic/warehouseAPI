using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserRepository
{
    public Task InsertAllFranchiseUsers(List<FranchiseUserEntity> franchiseUserEntities);

    public Task<bool> Exists(Guid userId);
    
    public Task<FranchiseUserEntity> GetFranchiseUserById(Guid userId);

    public Task UpdateFranchiseUser(FranchiseUserUpdateClass franchiseUserUpdateClass, Guid franchiseUserId); 
}