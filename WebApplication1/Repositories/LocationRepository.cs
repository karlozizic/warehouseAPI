using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly IContextService _contextService;
    
    public LocationRepository(IContextService contextService)
    {
        _contextService = contextService;
    }
    
    public async Task<List<LocationEntity>> GetLocations(Guid tenantId)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            return await warehouseContext.Location.ToListAsync();
        }
    }

    public async Task<Guid> Add(Guid tenantId, LocationEntity locationEntity)
    {
        using (var warehouseContext = _contextService.CreateDbContext(tenantId))
        {
            warehouseContext.Location.Add(locationEntity);
            await warehouseContext.SaveChangesAsync();
            return locationEntity.Id;
        }
    }
    // https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    // Implementacija IDisposable interface-a 
    /*private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _warehouseContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }*/
}