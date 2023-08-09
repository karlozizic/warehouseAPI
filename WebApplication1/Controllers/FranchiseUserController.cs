using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using X.Auth.Interface.Services;
using X.Auth.Middleware.Attributes;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class FranchiseUserController : ControllerBase
{
    private readonly ILogger<FranchiseUserController> _logger;
    private readonly IFranchiseUserService _franchiseUserService;
    private readonly IRetailService _retailService;
    private readonly IUserContextService _userContextService;
    
    public FranchiseUserController(ILogger<FranchiseUserController> logger, IFranchiseUserService franchiseUserService, 
        IRetailService retailService, IUserContextService userContextService)
    {
        _logger = logger;
        _franchiseUserService = franchiseUserService;
        _retailService = retailService;
        _userContextService = userContextService;
    }
    
    [VerifyGrants("backoffice")]
    [HttpPost(Name = "FetchFranchiseUsers")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchFranchiseUsers()
    {
        try
        {
            var franchiseUsers = await _retailService.FetchFranchiseUsers(_userContextService.UserContext.TenantId);
            await _franchiseUserService.InsertFranchiseUsers(franchiseUsers); 
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [VerifyGrants("backoffice")]
    [HttpGet(Name = "FetchFranchiseUser")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchFranchiseUserById([FromQuery] Guid franchiseUserId)
    {
        try
        { 
            var franchiseUser = await _franchiseUserService.GetFranchiseUserById(franchiseUserId);
            return Ok(franchiseUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [VerifyGrants("backoffice")]
    [HttpPost(Name="AssignOperator")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AssignOperator([FromQuery] Guid franchiseUserId, [FromQuery] Guid warehouseId)
    {
        try
        {
            await _franchiseUserService.AssignToWarehouse(franchiseUserId, warehouseId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}