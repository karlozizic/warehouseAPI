using WebApplication1.Database;

namespace WebApplication1.Interfaces;

public interface IContextService
{
    WarehouseContext CreateDbContext(Guid tenantId);
}