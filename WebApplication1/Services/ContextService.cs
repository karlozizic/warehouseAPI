using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;

namespace WebApplication1.Services;

public class ContextService
{
    private readonly string? _connectionString; 
    
    public ContextService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("WarehouseAppConnection");
    }

    public WarehouseContext CreateDbContext(Guid tenantId)
    {
        var builder = new DbContextOptionsBuilder<WarehouseContext>();
        builder.UseNpgsql(_connectionString);
        return new WarehouseContext(builder.Options, tenantId);
    }
}