using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Database;

public class WarehouseContext : DbContext
{
    private readonly Guid _tenantId;
    private readonly AppSettingsModel _appSettingsModel;
    public WarehouseContext(IOptions<AppSettingsModel> appSettingsModel)
    {
        _appSettingsModel = appSettingsModel.Value;
    }
    
    public WarehouseContext(DbContextOptions options, Guid tenantId, AppSettingsModel appSettingsModel) : base(options)
    {
        _tenantId = tenantId;
        _appSettingsModel = appSettingsModel;
    }
    
    public virtual DbSet<WarehouseEntity> Warehouse { get; set; }
    public virtual DbSet<LocationEntity> Location { get; set; }
    public virtual DbSet<ItemEntity> Item { get; set; }
    public virtual DbSet<FranchiseUserEntity> FranchiseUser { get; set; }
    
    public virtual DbSet<ItemRequestEntity> ItemRequest { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.HasDefaultSchema("warehouses");
        */

        modelBuilder.Entity<WarehouseEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id });
            entity.HasQueryFilter(t => t.TenantId == _tenantId);
            entity.UseXminAsConcurrencyToken();
        });
        
        modelBuilder.Entity<LocationEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
            entity.UseXminAsConcurrencyToken();
        });
        
        modelBuilder.Entity<ItemEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
            entity.UseXminAsConcurrencyToken();
        });
        
        modelBuilder.Entity<FranchiseUserEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
            entity.HasQueryFilter(t => t.TenantId == _tenantId);
            entity.UseXminAsConcurrencyToken();
        });
    
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_appSettingsModel.WarehouseConnectionString);
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging(); 
    }

}