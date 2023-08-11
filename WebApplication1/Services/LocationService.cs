using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    
    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    
    public async Task<List<LocationEntity>> GetLocations(Guid tenantId)
    {
        return await _locationRepository.GetLocations(tenantId);
    }
}