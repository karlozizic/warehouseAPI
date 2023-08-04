using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IWarehouseRepository : IDisposable
{
    public Task<List<Warehouse>> GetWarehouses();
    public Task<Warehouse?> GetWarehouseById(Guid id);
    public Task<Boolean> Exists(Guid id); 
    public Task InsertWarehouse(Warehouse warehouse);
    public Task DeleteWarehouse(Guid id);
    public Task UpdateWarehouse(Warehouse warehouse);
    public Task AssignOperator(FranchiseUser franchiseUser, Guid warehouseId);
    public Task<List<Item>> GetWarehouseItems(Guid warehouseId);
    public Task<FranchiseUser> GetOperator(Guid warehouseId); 
}