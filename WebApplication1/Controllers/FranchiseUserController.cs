using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class FranchiseUserController : ControllerBase
{
    private readonly ILogger<FranchiseUserController> _logger;
    private readonly IFranchiseUserService _franchiseUserService;
    /*private readonly IRetailService _retailService;*/
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public FranchiseUserController(ILogger<FranchiseUserController> logger, IFranchiseUserService franchiseUserService, 
        /*IRetailService retailService,*/ 
        IHubContext<NotificationHub> hubContext)
    {
        _logger = logger;
        _franchiseUserService = franchiseUserService;
        /*_retailService = retailService;*/
        _hubContext = hubContext;
    }
    //This endpoint fetches franchise users from Retail API
    /*[HttpPost(Name = "FetchFranchiseUsers")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchFranchiseUsers()
    {
        try
        {
            var franchiseUsers = await _retailService.FetchFranchiseUsers();
            await _franchiseUserService.InsertFranchiseUsers(franchiseUsers); 
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Fetch FranchiseUsers Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/

    [HttpGet(Name = "FetchFranchiseUser")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchFranchiseUserById([FromQuery] Guid tenantId, [FromQuery] Guid franchiseUserId)
    {
        try
        { 
            var franchiseUser = await _franchiseUserService.GetFranchiseUserById(tenantId, franchiseUserId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Fetch FranchiseUser Request");
            return Ok(franchiseUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost(Name="AssignOperator")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AssignOperator([FromQuery] Guid tenantId, [FromQuery] Guid franchiseUserId, [FromQuery] Guid warehouseId)
    {
        try
        {
            await _franchiseUserService.AssignToWarehouse(tenantId, franchiseUserId, warehouseId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Assign Operator Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPost(Name = "CreateFranchiseUser")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateFranchiseUser([FromBody] FranchiseUserDto franchiseUser)
    {
        try
        {
            await _franchiseUserService.InsertFranchiseUser(franchiseUser);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message); 
        }
    }
}