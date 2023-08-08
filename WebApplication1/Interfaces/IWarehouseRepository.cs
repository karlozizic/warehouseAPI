using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IWarehouseRepository : IDisposable
{
    public Task<List<WarehouseEntity>> GetWarehouses();
    public Task<WarehouseEntity?> GetWarehouseById(Guid id);
    public Task<Boolean> Exists(Guid id); 
    public Task InsertWarehouse(WarehouseEntity warehouseEntity);
    public Task DeleteWarehouse(Guid id);
    public Task UpdateWarehouse(WarehouseEntity warehouseEntity);
    public Task AssignOperator(FranchiseUserEntity franchiseUserEntity, Guid warehouseId);
    public Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId);
    public Task<FranchiseUserEntity> GetOperator(Guid warehouseId); 
    public Task InsertAllWarehouses(List<WarehouseEntity> warehouseEntities);
}