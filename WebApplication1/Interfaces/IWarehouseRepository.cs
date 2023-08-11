using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IWarehouseRepository
{
    public Task<List<WarehouseEntity>> GetWarehouses(Guid tenantId);
    public Task<WarehouseEntity?> GetWarehouseById(Guid tenantId, Guid id);
    public Task<Boolean> Exists(Guid tenantId, Guid id); 
    public Task InsertWarehouse(Guid tenantId, WarehouseEntity warehouseEntity);
    public Task DeleteWarehouse(Guid tenantId, Guid id);
    public Task UpdateWarehouse(Guid tenantId, WarehouseUpdateClass warehouseUpdateClass);
    /*
    public Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId);
    */
    public Task InsertAllWarehouses(Guid tenantId, List<WarehouseEntity> warehouseEntities);
}