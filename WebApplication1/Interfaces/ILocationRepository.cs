using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface ILocationRepository
{
    public Task<List<LocationEntity>> GetLocations(Guid tenantId);
    public Task<Guid> Add(Guid tenantId, LocationEntity locationEntity);
}