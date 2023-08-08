using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using CostCenterDto = X.Retail.Shared.Models.Models.Dtos.CostCenterDto;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IFranchiseUserRepository _franchiseUserRepository; 
    
    public WarehouseService(IWarehouseRepository warehouseRepository, IFranchiseUserRepository franchiseUserRepository)
    {
        _warehouseRepository = warehouseRepository; 
        _franchiseUserRepository = franchiseUserRepository;
    }
    
    public async Task<List<WarehouseEntity>> GetWarehouses()
    {
        return await _warehouseRepository.GetWarehouses();
    }
    
    public async Task<WarehouseEntity> GetWarehouseById(Guid id)
    {
        if (!await _warehouseRepository.Exists(id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        return await _warehouseRepository.GetWarehouseById(id);
    }
    
    
    public async Task InsertWarehouse(WarehouseEntity warehouseEntity)
    {
        /*if (await _warehouseRepository.Exists(warehouse.Id))
        {
            throw new Exception("Warehouse already exists"); 
        }*/
        
        await _warehouseRepository.InsertWarehouse(warehouseEntity);
    }
    
    public async Task DeleteWarehouse(Guid id)
    {
        if (!await _warehouseRepository.Exists(id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.DeleteWarehouse(id);
    }
    
    public async Task UpdateWarehouse(WarehouseUpdateClass warehouseUpdateClass)
    {
        var id = warehouseUpdateClass.Id; 
        if (!await _warehouseRepository.Exists(id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.UpdateWarehouse(warehouseUpdateClass);
    }
    
    public async Task AssignOperator(Guid franchiseUserId, Guid warehouseId)
    {
        WarehouseEntity warehouse = await _warehouseRepository.GetWarehouseById(warehouseId);
        if (warehouse == null)
        {
            throw new Exception("Warehouse does not exist");
        }
        
        FranchiseUserEntity franchiseUserEntity = await _franchiseUserRepository.GetFranchiseUserById(franchiseUserId);
        
        if(franchiseUserEntity == null)
        {
            throw new Exception("Franchise user does not exist");
        }

        var warehouseUpdateClass = new WarehouseUpdateClass();
        warehouseUpdateClass.Id = warehouseId;
        warehouseUpdateClass.OperatorUser = franchiseUserEntity;
        
        await _warehouseRepository.UpdateWarehouse(warehouseUpdateClass);
        
    }
    
    public async Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name)
    {
        if (!await _warehouseRepository.Exists(warehouseId))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        List<ItemEntity> items = await _warehouseRepository.GetWarehouseItems(warehouseId);

        if (name != null)
        {
            List<ItemEntity> filteredItems = items.Where(item => item.Name == name).ToList();
            return filteredItems;
        }

        return items; 
    }
    
    public async Task InsertWarehouses(List<CostCenterDto> warehouses)
    {
        List<WarehouseEntity> warehouseEntities = new List<WarehouseEntity>();

        foreach (var warehouse in warehouses)
        {
            warehouseEntities.Add(new WarehouseEntity(warehouse.Id, warehouse.Name, warehouse.PhoneNumber,
                warehouse.Code, warehouse.Deleted, warehouse.DefaultLanguage, warehouse.IsPayoutLockedForOtherCostCenter));
        }
        
        await _warehouseRepository.InsertAllWarehouses(warehouseEntities);

    }
}