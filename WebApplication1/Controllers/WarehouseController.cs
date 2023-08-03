using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger; 
    /*private readonly WarehouseContext _warehouseContext;*/
    private IWarehouseService _warehouseService; 
    public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    [HttpGet(Name = "GetWarehouses")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var warehouses =  await _warehouseService.GetWarehouses();
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
    public async Task<IActionResult> Get(Guid id)
    {
        try
        { 
            var warehouse = await _warehouseService.GetWarehouseById(id);
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
    public async Task<IActionResult> Insert([FromBody] Warehouse warehouse)
    {
        //nije potrebno ModelState.IsValid jer se automatski validira 
        try
        {
            await _warehouseService.InsertWarehouse(warehouse);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("id", Name = "DeleteWarehouse")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _warehouseService.DeleteWarehouse(id);
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
    public async Task<IActionResult> Update([FromBody] Warehouse warehouse)
    {
        try
        {
            await _warehouseService.UpdateWarehouse(warehouse);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}