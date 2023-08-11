using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface ILocationRepository
{
    public Task<List<LocationEntity>> GetLocations(Guid tenantId);
    public Task<Guid> Add(LocationEntity locationEntity, Guid tenantId);
}