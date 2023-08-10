using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using X.Auth.Middleware.Attributes;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly IItemService _itemService;
    private readonly IWarehouseService _warehouseService;
    
    public ItemController(ILogger<ItemController> logger, IItemService itemService, IWarehouseService warehouseService)
    {
        _logger = logger;
        _itemService = itemService;
        _warehouseService = warehouseService;
    }
    
    [VerifyGrants("backoffice")]
    [HttpPut(Name = "UpdateWarehouseItem")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateWarehouseItem([FromBody] ItemEntity warehouseItemEntity)
    {
        try
        {
            await _itemService.UpdateItem(warehouseItemEntity); 
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [VerifyGrants("backoffice")]
    [HttpGet(Name = "GetItem")]
    [ProducesResponseType(typeof(List<ItemEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetItem([FromQuery] Guid id)
    {
        try
        {
            var item = await _itemService.GetItemById(id);
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}