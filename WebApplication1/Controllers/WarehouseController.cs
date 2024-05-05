using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    /*private readonly IRetailService _retailService;*/
    private readonly IHubContext<NotificationHub> _hubContext;

    public WarehouseController(IWarehouseService warehouseService, 
        /*IRetailService retailService,*/ 
        IHubContext<NotificationHub> hubContext)
    {
        _warehouseService = warehouseService;
        /*_retailService = retailService;*/
        _hubContext = hubContext;
    }
    
    [HttpGet(Name = "GetWarehouses")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetWarehouses([FromQuery] Guid tenantId)
    {
        try
        {
            var warehouses =  await _warehouseService.GetWarehouses(tenantId);
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
    public async Task<IActionResult> Get([FromQuery] Guid tenantId, [FromQuery] Guid id)
    {
        try
        { 
            var warehouse = await _warehouseService.GetWarehouseById(tenantId, id);
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
    public async Task<IActionResult> Insert([FromQuery] Guid tenantId, [FromBody] WarehouseDto warehouse)
    {
        // nije potrebno ModelState.IsValid jer se automatski validira 
        try
        {
            WarehouseDto warehouseDto = await _warehouseService.InsertWarehouse(tenantId, warehouse);
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
    public async Task<IActionResult> Delete([FromQuery] Guid tenantId,
        [FromQuery] Guid id)
    {
        try
        {
            await _warehouseService.DeleteWarehouse(tenantId, id);
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
    public async Task<IActionResult> Update([FromQuery] Guid tenantId,
        [FromBody] WarehouseUpdateClass warehouseEntity)
    {
        try
        {
            await _warehouseService.UpdateWarehouse(tenantId, warehouseEntity);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Update Warehouse Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /*[HttpPost(Name = "FetchWarehouses")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FetchWarehouses(
        [FromQuery] Guid tenantId,
        [FromQuery] string? name,
        [FromQuery] string? city)
    {
        try
        {
            var warehouses = await _retailService.FetchWarehouses(tenantId, name, city);
            await _warehouseService.InsertWarehouses(tenantId, warehouses);   
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Fetch Warehouses Request");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/
    
}