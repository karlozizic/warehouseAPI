using WebApplication1.Database.Entities;
using WebApplication1.Models;
using CostCenterDto = X.Retail.Shared.Models.Models.Dtos.CostCenterDto;

namespace WebApplication1.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseEntity>> GetWarehouses(Guid userContextTenantId);
    Task<WarehouseEntity> GetWarehouseById(Guid id);
    Task InsertWarehouse(WarehouseEntity warehouseEntity);
    Task DeleteWarehouse(Guid id);
    Task UpdateWarehouse(WarehouseUpdateClass warehouseEntity);
    
    /*
    Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name);
    */
    
    Task InsertWarehouses(List<CostCenterDto> warehouses);

}