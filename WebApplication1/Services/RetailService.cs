using Newtonsoft.Json;
using WebApplication1.Constants;
using WebApplication1.Models;
using X.Consul.Interface.Constants;
using X.Consul.Interface.Services;
using X.Retail.Shared.Models.Filters;
using CostCenterDto = X.Retail.Shared.Models.Models.Dtos.CostCenterDto;

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
    public async Task<List<X.Retail.Shared.Models.Models.Dtos.CostCenterDto>> FetchWarehouses(Guid tenantId, string? name, string? city)
    {
        
        var payload = new CostCenterFilter()
        {
            TenantId = tenantId,
            PartialName = name,
            PartialCity = city
        };

        //if response.IsSuccessStatusCode

        return await _requestService.PostWithDiscovery<List<CostCenterDto>>(
            Guid.Empty, 
            ApiNames.RetailApi,
            Endpoints.CostCenters,
            payload: payload
            );
    }
    
    
}

public interface IRetailService
{   
    Task<List<X.Retail.Shared.Models.Models.Dtos.CostCenterDto>> FetchWarehouses(Guid tenantId, string? name, string? city);
}