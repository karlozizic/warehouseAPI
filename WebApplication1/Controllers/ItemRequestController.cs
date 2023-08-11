using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Services;
using X.Auth.Interface.Services;
using X.Auth.Middleware.Attributes;

namespace WebApplication1.Controllers;


[ApiController]
[Route("api/public/[controller]/[action]")]
public class ItemRequestController : ControllerBase
{
    private readonly IItemRequestService _itemRequestService;
    private readonly IUserContextService _userContextService;
    
    public ItemRequestController(IItemRequestService itemRequestService, IUserContextService userContextService)
    {
        _itemRequestService = itemRequestService;
        _userContextService = userContextService;
    }
    
    [HttpGet(Name = "GetItemRequests")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var itemRequests =  await _itemRequestService.GetItemRequests(_userContextService.UserContext.TenantId);
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
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        try
        { 
            var itemRequest = await _itemRequestService.GetItemRequestById(_userContextService.UserContext.TenantId, id);
            return Ok(itemRequest);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [VerifyGrants("backoffice")]
    [HttpPost(Name = "CreateItemRequest")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] ItemRequestDto itemRequest)
    {
        try
        {   
            //operatorId se saznaje iz _userContextService
            Guid operatorId = _userContextService.UserContext.UserId; 
            await _itemRequestService.CreateItemRequest(_userContextService.UserContext.TenantId, itemRequest, operatorId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [VerifyGrants("backoffice")]
    [HttpPut(Name = "ItemRequestUpdate")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] Guid itemRequestId, [FromQuery] String itemStatus)
    {
        try
        {   
            Guid operatorId = _userContextService.UserContext.UserId;
            ItemRequestEnum status = (ItemRequestEnum) Enum.Parse(typeof(ItemRequestEnum), itemStatus);
            await _itemRequestService.UpdateItemRequest(_userContextService.UserContext.TenantId, itemRequestId, status, operatorId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [VerifyGrants("backoffice")]
    [HttpDelete(Name = "DeleteItemRequest")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete([FromQuery] Guid itemRequestId)
    {
        try
        {   
            await _itemRequestService.DeleteItemRequest(_userContextService.UserContext.TenantId, itemRequestId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}