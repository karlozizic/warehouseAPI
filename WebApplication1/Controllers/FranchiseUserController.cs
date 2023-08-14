using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;
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
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public FranchiseUserController(ILogger<FranchiseUserController> logger, IFranchiseUserService franchiseUserService, 
        IRetailService retailService, IUserContextService userContextService, IHubContext<NotificationHub> hubContext)
    {
        _logger = logger;
        _franchiseUserService = franchiseUserService;
        _retailService = retailService;
        _userContextService = userContextService;
        _hubContext = hubContext;
    }
    
    [VerifyGrants("backoffice")]
    [HttpGet(Name = "FetchFranchiseUsers")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchFranchiseUsers()
    {
        try
        {
            var franchiseUsers = await _retailService.FetchFranchiseUsers(_userContextService.UserContext.TenantId);
            await _franchiseUserService.InsertFranchiseUsers(_userContextService.UserContext.TenantId, franchiseUsers); 
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Fetch FranchiseUsers Request");
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
            var franchiseUser = await _franchiseUserService.GetFranchiseUserById(_userContextService.UserContext.TenantId, franchiseUserId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Fetch FranchiseUser Request");
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
            await _franchiseUserService.AssignToWarehouse(_userContextService.UserContext.TenantId, franchiseUserId, warehouseId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Assign Operator Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}