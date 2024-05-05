using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserService
{
    Task InsertFranchiseUsers(Guid tenantId, List<FranchiseUserDto> franchiseUsers);

    Task InsertFranchiseUser(FranchiseUserDto franchiseUserDto);
    
    Task AssignToWarehouse(Guid tenantId, Guid franchiseUserId, Guid warehouseId);
    
    Task<FranchiseUserEntity> GetFranchiseUserById(Guid tenantId, Guid userId);
    
}