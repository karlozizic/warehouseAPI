using WebApplication1.Database.Entities;
using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseEntity>> GetWarehouses();
    Task<WarehouseEntity> GetWarehouseById(Guid id);
    Task InsertWarehouse(WarehouseEntity warehouseEntity);
    Task DeleteWarehouse(Guid id);
    Task UpdateWarehouse(WarehouseEntity warehouseEntity);
    Task AssignOperator(FranchiseUserEntity franchiseUserEntity, Guid warehouseId);
    
    Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name);
    
    Task InsertWarehouses(List<CostCenterDto> warehouses);

}