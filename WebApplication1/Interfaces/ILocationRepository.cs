using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface ILocationRepository : IDisposable
{
    public Task<List<Location>> GetLocations();
}