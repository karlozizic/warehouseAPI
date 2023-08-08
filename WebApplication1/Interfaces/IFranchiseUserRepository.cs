using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserRepository
{
    public Task InsertAllFranchiseUsers(List<FranchiseUserEntity> franchiseUserEntities); 

}