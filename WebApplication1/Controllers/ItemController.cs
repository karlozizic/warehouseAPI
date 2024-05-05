using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Database.Entities;
using WebApplication1.Hubs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public ItemController(IItemService itemService,
        IHubContext<NotificationHub> hubContext)
    {
        _itemService = itemService;
        _hubContext = hubContext;
    }
    
    [HttpGet(Name = "GetItem")]
    [ProducesResponseType(typeof(List<ItemEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetItem([FromQuery] Guid tenantId, [FromQuery] Guid id)
    {
        try
        {
            var item = await _itemService.GetItemById(tenantId, id);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get Item");
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet(Name = "GetWarehouseItems")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetWarehouseItems([FromQuery] Guid tenantId, [FromQuery] Guid warehouseId, [FromQuery] String? name)
    {
        try
        {
            var items = await _itemService.GetWarehouseItems(tenantId, warehouseId, name);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get Warehouse Items");
            return Ok(items);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPut(Name = "UpdateWarehouseItem")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateWarehouseItem([FromQuery] Guid tenantId, [FromBody] ItemEntity warehouseItemEntity)
    {
        try
        {
            await _itemService.UpdateItem(tenantId, warehouseItemEntity); 
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Update Warehouse Item");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPut(Name = "ReserveWarehouseItem")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> ReserveWarehouseItem([FromQuery] Guid tenantId, 
        [FromQuery] Guid userId, [FromQuery] Guid itemId)
    {
        try
        {
            await _itemService.ReserveItem(tenantId, userId, itemId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Reserve Warehouse Item");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}