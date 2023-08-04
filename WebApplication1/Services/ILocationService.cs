using WebApplication1.Database.Entities;

namespace WebApplication1.Services;

public interface ILocationService
{
    Task<List<Location>> GetLocations();
}