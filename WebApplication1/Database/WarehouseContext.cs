using Microsoft.EntityFrameworkCore;
using WebApplication1.Database.Entities;

namespace WebApplication1.Database;

public class WarehouseContext : DbContext
{   
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<FranchiseUser> FranchiseUsers { get; set; }
    public DbSet<User> Users { get; set; }

    IConfigurationRoot _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json")
        .Build();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WarehouseAppConnection"));
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging(); 
    }

}