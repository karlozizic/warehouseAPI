using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Database.Entities;
using WebApplication1.Hubs;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using X.Auth.Interface.Services;
using X.Auth.Middleware.Attributes;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/public/[controller]/[action]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IUserContextService _userContextService;
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public ItemController(IItemService itemService,
        IUserContextService userContextService, IHubContext<NotificationHub> hubContext)
    {
        _itemService = itemService;
        _userContextService = userContextService;
        _hubContext = hubContext;
    }
    
    [VerifyGrants("backoffice")]
    [HttpGet(Name = "GetItem")]
    [ProducesResponseType(typeof(List<ItemEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetItem([FromQuery] Guid id)
    {
        try
        {
            var item = await _itemService.GetItemById(_userContextService.UserContext.TenantId, id);
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
    public async Task<IActionResult> GetWarehouseItems([FromQuery] Guid warehouseId, [FromQuery] String? name)
    {
        try
        {
            var items = await _itemService.GetWarehouseItems(_userContextService.UserContext.TenantId, warehouseId, name);
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
    public async Task<IActionResult> UpdateWarehouseItem([FromBody] ItemEntity warehouseItemEntity)
    {
        try
        {
            await _itemService.UpdateItem(_userContextService.UserContext.TenantId, warehouseItemEntity); 
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
    public async Task<IActionResult> ReserveWarehouseItem([FromQuery] Guid itemId)
    {
        try
        {
            await _itemService.ReserveItem(_userContextService.UserContext.TenantId, _userContextService.UserContext.UserId, itemId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Reserve Warehouse Item");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}