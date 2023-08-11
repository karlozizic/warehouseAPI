using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;
using X.Auth.Interface.Services;
using X.Auth.Middleware.Attributes;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class WarehouseController : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger; 
    /*private readonly WarehouseContext _warehouseContext;*/
    private readonly IWarehouseService _warehouseService;
    private readonly IUserContextService _userContextService;
    private readonly IRetailService _retailService;

    public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService, 
        IUserContextService userContextService, IRetailService retailService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
        _userContextService = userContextService;
        _retailService = retailService;
    }
    
    [HttpGet(Name = "GetWarehouses")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var warehouses =  await _warehouseService.GetWarehouses(_userContextService.UserContext.TenantId);
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
    public async Task<IActionResult> Insert([FromBody] WarehouseEntity warehouseEntity)
    {
        //nije potrebno ModelState.IsValid jer se automatski validira 
        try
        {
            await _warehouseService.InsertWarehouse(_userContextService.UserContext.TenantId, warehouseEntity);
            return Ok(warehouseEntity);
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
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /*[VerifyGrants("backoffice")]
    [HttpGet(Name = "GetWarehouseItems")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetWarehouseItems([FromQuery] Guid warehouseId, [FromQuery] String? name)
    {
        try
        {
            var items = await _warehouseService.GetWarehouseItems(warehouseId, name);
            //var user = _userContextService.UserContext; 
            return Ok(items);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/

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
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}