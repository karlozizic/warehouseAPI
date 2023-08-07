using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface ILocationService
{
    Task<List<LocationEntity>> GetLocations();
}