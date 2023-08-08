using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Interfaces;

public interface IFranchiseUserService
{
    Task InsertFranchiseUsers(List<FranchiseUserDto> franchiseUsers);
    
}