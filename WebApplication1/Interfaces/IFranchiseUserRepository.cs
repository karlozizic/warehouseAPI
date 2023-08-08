using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserRepository
{
    public Task InsertAllFranchiseUsers(List<FranchiseUserEntity> franchiseUserEntities);

    public Task<bool> Exists(Guid userId);
    
    public Task<FranchiseUserEntity> GetFranchiseUserById(Guid userId);
}