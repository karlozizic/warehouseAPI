using WebApplication1.Database.Entities;

namespace WebApplication1.Repositories;

public interface IWarehouseRepository : IDisposable
{
    public Task<List<Warehouse>> GetWarehouses();
    public Task<Warehouse?> GetWarehouseById(Guid id);
    public Task<Boolean> Exists(Guid id); 
    public Task InsertWarehouse(Warehouse warehouse);
    public Task DeleteWarehouse(Guid id);
    public Task UpdateWarehouse(Warehouse warehouse);
}