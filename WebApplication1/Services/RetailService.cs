//This service was used to fetch Warehouses and FranchiseUsers from another Retail API
/*using System.Net;
using WebApplication1.Constants;
using WebApplication1.Models;
using X.Consul.Interface.Constants;
using X.Consul.Interface.Services;
using X.Retail.Shared.Models.Filters;
using X.Retail.Shared.Models.Models.Dtos;
using X.Retail.Shared.Models.Requests;

namespace WebApplication1.Services;

public class RetailService : IRetailService
{
    private readonly IRequestService _requestService;
    private readonly HttpClient _httpClient;
    
    public RetailService(IRequestService requestService, HttpClient httpClient)
    {
        _requestService = requestService;
        _httpClient = httpClient;
        
    }
    public async Task<List<X.Retail.Shared.Models.Models.Dtos.WarehouseDto>> FetchWarehouses(Guid tenantId, string? name, string? city)
    {
        
        var payload = new CostCenterFilter()
        {
            TenantId = tenantId,
            PartialName = name,
            PartialCity = city
        };

        return await _requestService.PostWithDiscovery<List<WarehouseDto>>(
            Guid.Empty, 
            ApiNames.RetailApi,
            Endpoints.CostCenters,
            payload: payload
            );
    }
    
    public async Task<List<FranchiseUserDto>> FetchFranchiseUsers(Guid tenantId)
    {
        var payload = new FranchiseUsersFilterRequest()
        {
            TenantId = tenantId
        };

        var retailUrlPort = await _requestService.FetchServiceDetails(Guid.Empty, ApiNames.RetailApi);
        string url = retailUrlPort + Endpoints.FranchiseUsers + "?tenantId=" + tenantId; 
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<FranchiseUserDto>();
            }

            List<FranchiseUserDto> franchiseUsers = response.Content.ReadFromJsonAsync<List<FranchiseUserDto>>().Result;
            return franchiseUsers;
        }
        
        throw new Exception("Error fetching franchise users");
        
        
    }
    


}

public interface IRetailService
{   
    Task<List<WarehouseDto>> FetchWarehouses(Guid tenantId, string? name, string? city);
    Task<List<WarehouseDto>> FetchFranchiseUsers(Guid tenantId);
}*/