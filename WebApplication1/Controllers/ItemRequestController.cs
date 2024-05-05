using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Enums;
using WebApplication1.Hubs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[ApiController]
[Route("api/public/[controller]/[action]")]
public class ItemRequestController : ControllerBase
{
    private readonly IItemRequestService _itemRequestService;
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public ItemRequestController(IItemRequestService itemRequestService,
        IHubContext<NotificationHub> hubContext)
    {
        _itemRequestService = itemRequestService;
        _hubContext = hubContext;
    }
    
    [HttpGet(Name = "GetItemRequests")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] Guid tenantId)
    {
        try
        {
            var itemRequests =  await _itemRequestService.GetItemRequests(tenantId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get ItemRequests");
            return Ok(itemRequests);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("id", Name = "GetItemRequestById")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById([FromQuery] Guid id,
        [FromQuery] Guid tenantId)
    {
        try
        { 
            var itemRequest = await _itemRequestService.GetItemRequestById(tenantId, id);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get ItemRequest By Id");
            return Ok(itemRequest);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost(Name = "CreateItemRequest")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] ItemRequestDto itemRequest)
    {
        try
        {   
            //operatorId se saznaje iz _userContextService
            await _itemRequestService.CreateItemRequest(itemRequest);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Create ItemRequest");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut(Name = "ItemRequestUpdate")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] Guid tenantId,
        [FromQuery] Guid operatorId, 
        [FromQuery] Guid itemRequestId,
        [FromQuery] String itemStatus)
    {
        try
        {   
            ItemRequestEnum status = (ItemRequestEnum) Enum.Parse(typeof(ItemRequestEnum), itemStatus);
            await _itemRequestService.UpdateItemRequest(tenantId, itemRequestId, status, operatorId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get ItemRequests");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete(Name = "DeleteItemRequest")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete([FromQuery] Guid tenantId, [FromQuery] Guid itemRequestId)
    {
        try
        {   
            await _itemRequestService.DeleteItemRequest(tenantId, itemRequestId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Delete ItemRequest");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}