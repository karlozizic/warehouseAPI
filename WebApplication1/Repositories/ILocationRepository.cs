using WebApplication1.Database.Entities;

namespace WebApplication1.Repositories;

public interface ILocationRepository : IDisposable
{
    public Task<List<Location>> GetLocations();
}