using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers;


[ApiController]
[Route("api/public/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationService _locationService;
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public LocationController(ILogger<LocationController> logger, ILocationService locationService, 
        IHubContext<NotificationHub> hubContext)
    {
        _logger = logger;
        _locationService = locationService;
        _hubContext = hubContext;
    }
    
    [HttpGet(Name = "GetLocations")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] Guid tenantId)
    {
        try
        {
            var locations =  await _locationService.GetLocations(tenantId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Get Locations Request");
            return Ok(locations);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}