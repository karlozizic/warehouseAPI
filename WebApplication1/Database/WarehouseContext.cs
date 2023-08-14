using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Database;

public class WarehouseContext : DbContext
{
    private readonly Guid _tenantId;

    public WarehouseContext()
    {
        
    }
    
    public WarehouseContext(DbContextOptions options, Guid tenantId) : base(options)
    {
        _tenantId = tenantId;
    }
    
    public virtual DbSet<WarehouseEntity> Warehouse { get; set; }
    public virtual DbSet<LocationEntity> Location { get; set; }
    public virtual DbSet<ItemEntity> Item { get; set; }
    public virtual DbSet<FranchiseUserEntity> FranchiseUser { get; set; }
    
    public virtual DbSet<ItemRequestEntity> ItemRequest { get; set; }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("warehouses");

        modelBuilder.Entity<WarehouseEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
            entity.HasQueryFilter(t => t.TenantId == _tenantId);
        });
        
        modelBuilder.Entity<LocationEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
        });
        
        modelBuilder.Entity<ItemEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
            entity.HasQueryFilter(t => t.TenantId == _tenantId);
        });
        
        modelBuilder.Entity<FranchiseUserEntity>(entity =>
        {
            entity.HasKey(t => new { t.Id }); 
            entity.HasQueryFilter(t => t.TenantId == _tenantId);
        });
    
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging(); 
    }

}