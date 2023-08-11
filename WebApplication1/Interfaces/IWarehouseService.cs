using WebApplication1.Database.Entities;
using WebApplication1.Models;
using CostCenterDto = X.Retail.Shared.Models.Models.Dtos.CostCenterDto;

namespace WebApplication1.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseEntity>> GetWarehouses(Guid tenantId);
    Task<WarehouseEntity> GetWarehouseById(Guid tenantId, Guid id);
    Task InsertWarehouse(Guid tenantId, WarehouseEntity warehouseEntity);
    Task DeleteWarehouse(Guid tenantId, Guid id);
    Task UpdateWarehouse(Guid tenantId, WarehouseUpdateClass warehouseEntity);
    
    /*
    Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name);
    */
    
    Task InsertWarehouses(Guid tenantId, List<CostCenterDto> warehouses);

}