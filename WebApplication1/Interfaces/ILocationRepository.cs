using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface ILocationRepository : IDisposable
{
    public Task<List<LocationEntity>> GetLocations();
    public Task<Guid> Add(LocationEntity locationEntity);
}