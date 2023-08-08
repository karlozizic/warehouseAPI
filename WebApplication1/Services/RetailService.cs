﻿using System.Net;
using WebApplication1.Constants;
using X.Consul.Interface.Constants;
using X.Consul.Interface.Services;
using X.Retail.Shared.Models.Filters;
using X.Retail.Shared.Models.Models.Dtos;
using X.Retail.Shared.Models.Requests;
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

        return await _requestService.PostWithDiscovery<List<CostCenterDto>>(
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
        else
        {
            throw new Exception("Error fetching franchise users");
        }
        
    }
    


}

public interface IRetailService
{   
    Task<List<X.Retail.Shared.Models.Models.Dtos.CostCenterDto>> FetchWarehouses(Guid tenantId, string? name, string? city);
    Task<List<X.Retail.Shared.Models.Models.Dtos.FranchiseUserDto>> FetchFranchiseUsers(Guid tenantId);
}