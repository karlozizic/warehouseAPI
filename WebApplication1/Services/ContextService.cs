using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ContextService : IContextService
{
    private readonly AppSettingsModel _appSettingsModel;

    public ContextService(IOptions<AppSettingsModel> appSettingsModel)
    {
        _appSettingsModel = appSettingsModel.Value;
    }

    public WarehouseContext CreateDbContext(Guid tenantId)
    {
        var builder = new DbContextOptionsBuilder<WarehouseContext>();
        builder.UseNpgsql(_appSettingsModel.WarehouseConnectionString);
        return new WarehouseContext(builder.Options, tenantId);
    }
}

public interface IContextService
{
    WarehouseContext CreateDbContext(Guid tenantId);

}