using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[ApiController]
[Route("api/public/[controller]/[action]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationService _locationService;
    
    public LocationController(ILogger<LocationController> logger, ILocationService locationService)
    {
        _logger = logger;
        _locationService = locationService;
    }
    
    [HttpGet(Name = "GetLocations")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var locations =  await _locationService.GetLocations();
            return Ok(locations);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}