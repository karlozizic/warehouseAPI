using WebApplication1.Database.Entities;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IWarehouseRepository
{
    public Task<List<WarehouseEntity>> GetWarehouses(Guid tenantId);
    public Task<WarehouseEntity?> GetWarehouseById(Guid id, Guid tenantId);
    public Task<Boolean> Exists(Guid id, Guid tenantId); 
    public Task InsertWarehouse(WarehouseEntity warehouseEntity, Guid tenantId);
    public Task DeleteWarehouse(Guid id, Guid tenantId);
    public Task UpdateWarehouse(WarehouseUpdateClass warehouseUpdateClass, Guid tenantId);
    /*
    public Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId);
    */
    public Task InsertAllWarehouses(List<WarehouseEntity> warehouseEntities, Guid tenantId);
}