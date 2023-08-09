using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using X.Auth.Middleware.Attributes;

namespace WebApplication1.Controllers;


[ApiController]
[Route("api/public/[controller]/[action]")]
public class ItemRequestController : ControllerBase
{
    private readonly ILogger<ItemRequestController> _logger;
    private readonly IItemRequestService _itemRequestService;
    
    public ItemRequestController(ILogger<ItemRequestController> logger, IItemRequestService itemRequestService)
    {
        _logger = logger;
        _itemRequestService = itemRequestService;
    }
    
    [HttpGet(Name = "GetItemRequests")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var itemRequests =  await _itemRequestService.GetItemRequests();
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
            var itemRequest = await _itemRequestService.GetItemRequestById(id);
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
            await _itemRequestService.CreateItemRequest(itemRequest);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}