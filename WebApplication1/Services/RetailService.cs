using Newtonsoft.Json;
using WebApplication1.Constants;
using WebApplication1.Models;
using X.Consul.Interface.Constants;
using X.Consul.Interface.Services;
using X.Retail.Shared.Models.Filters;

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
    // potrebno je dodati CostCenterFilter
    public async Task<List<CostCenterDto>> FetchLocations(Guid tenantId, string? name, string? city, CostCenterFilter filter = null)
    {
        var retailUrlPort = await _requestService.FetchServiceDetails(Guid.Empty, ApiNames.RetailApi);
        
        var payload = filter ?? new CostCenterFilter()
        {
            TenantId = tenantId,
            PartialName = name,
            PartialCity = city
        };

        var response = await _httpClient.PostAsJsonAsync(Url.RetailUrl + retailUrlPort + Endpoints.CostCenters, payload); 
        
        if (response.IsSuccessStatusCode)
        {
            List<CostCenterDto> locations = response.Content.ReadFromJsonAsync<List<CostCenterDto>>().Result;
            return locations;
        }
        
        return new List<CostCenterDto>();
    }
}

public interface IRetailService
{   
    Task<List<CostCenterDto>> FetchLocations(Guid tenantId, string? name, string? city, CostCenterFilter filter);
}