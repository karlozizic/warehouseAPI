using WebApplication1.Database.Entities;
using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Interfaces;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetWarehouses();
    Task<Warehouse> GetWarehouseById(Guid id);
    Task InsertWarehouse(Warehouse warehouse);
    Task DeleteWarehouse(Guid id);
    Task UpdateWarehouse(Warehouse warehouse);
    Task AssignOperator(FranchiseUser franchiseUser, Guid warehouseId);
    
    Task<List<Item>> GetWarehouseItems(Guid warehouseId, String? name);
    
    Task InsertWarehouses(List<CostCenterDto> warehouses);

}