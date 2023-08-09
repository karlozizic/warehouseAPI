using WebApplication1.Database.Entities;
using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserService
{
    Task InsertFranchiseUsers(List<FranchiseUserDto> franchiseUsers);
    
    Task AssignToWarehouse(Guid franchiseUserId, Guid warehouseId);
    
    Task<FranchiseUserEntity> GetFranchiseUserById(Guid userId);
    
}