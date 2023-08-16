using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Database.Entities;
using WebApplication1.Hubs;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;
using X.Auth.Interface.Services;
using X.Auth.Middleware.Attributes;
using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    private readonly IUserContextService _userContextService;
    private readonly IRetailService _retailService;
    private readonly IHubContext<NotificationHub> _hubContext;

    public WarehouseController(IWarehouseService warehouseService, 
        IUserContextService userContextService, IRetailService retailService, IHubContext<NotificationHub> hubContext)
    {
        _warehouseService = warehouseService;
        _userContextService = userContextService;
        _retailService = retailService;
        _hubContext = hubContext;
    }
    
    [HttpGet(Name = "GetWarehouses")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var warehouses =  await _warehouseService.GetWarehouses(_userContextService.UserContext.TenantId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get Warehouses Request");
            return Ok(warehouses);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("id", Name = "GetWarehouseById")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] Guid id)
    {
        try
        { 
            var warehouse = await _warehouseService.GetWarehouseById(_userContextService.UserContext.TenantId, id);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get Warehouse By Id Request");

            return Ok(warehouse);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost(Name = "InsertWarehouse")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Insert([FromBody] CostCenterDto warehouseEntity)
    {
        // nije potrebno ModelState.IsValid jer se automatski validira 
        try
        {
            CostCenterDto warehouseDto = await _warehouseService.InsertWarehouse(_userContextService.UserContext.TenantId, warehouseEntity);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Insert Warehouse Request");
            return Ok(warehouseDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("id", Name = "DeleteWarehouse")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        try
        {
            await _warehouseService.DeleteWarehouse(_userContextService.UserContext.TenantId, id);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Delete Warehouse Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut(Name = "UpdateWarehouse")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] WarehouseUpdateClass warehouseEntity)
    {
        try
        {
            await _warehouseService.UpdateWarehouse(_userContextService.UserContext.TenantId, warehouseEntity);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Update Warehouse Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [VerifyGrants("backoffice")]
    [HttpPost(Name = "FetchWarehouses")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchWarehouses([FromQuery] string? name, [FromQuery] string? city)
    {
        try
        {
            var warehouses = await _retailService.FetchWarehouses(_userContextService.UserContext.TenantId, name, city);
            await _warehouseService.InsertWarehouses(_userContextService.UserContext.TenantId, warehouses);   
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Fetch Warehouses Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}