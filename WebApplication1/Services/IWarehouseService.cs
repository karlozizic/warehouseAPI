using WebApplication1.Database.Entities;

namespace WebApplication1.Services;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetWarehouses();
    Task<Warehouse> GetWarehouseById(Guid id);
    Task InsertWarehouse(Warehouse warehouse);
    Task DeleteWarehouse(Guid id);
    Task UpdateWarehouse(Warehouse warehouse);
    
}