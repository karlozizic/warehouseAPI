using WebApplication1.Database.Entities;
using WebApplication1.Models;
using CostCenterDto = X.Retail.Shared.Models.Models.Dtos.CostCenterDto;

namespace WebApplication1.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseEntity>> GetWarehouses(Guid tenantId);
    Task<WarehouseEntity> GetWarehouseById(Guid id, Guid tenantId);
    Task InsertWarehouse(WarehouseEntity warehouseEntity, Guid tenantId);
    Task DeleteWarehouse(Guid id, Guid tenantId);
    Task UpdateWarehouse(WarehouseUpdateClass warehouseEntity, Guid tenantId);
    
    /*
    Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name);
    */
    
    Task InsertWarehouses(List<CostCenterDto> warehouses, Guid tenantId);

}