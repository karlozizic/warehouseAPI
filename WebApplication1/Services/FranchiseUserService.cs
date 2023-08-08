using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
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
                franchiseUser.UserId, franchiseUser.FranchiseId, franchiseUser.Username, franchiseUser.TenantId));
        }
        
        await _franchiseUserRepository.InsertAllFranchiseUsers(franchiseUserEntities);
    }
    
}