using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using X.Retail.Shared.Models.Models.Dtos;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ILocationRepository _locationRepository;
    
    public WarehouseService(IWarehouseRepository warehouseRepository, ILocationRepository locationRepository)
    {
        _warehouseRepository = warehouseRepository;
        _locationRepository = locationRepository;
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
    
    public async Task UpdateWarehouse(WarehouseEntity warehouseEntity)
    {
        if (!await _warehouseRepository.Exists(warehouseEntity.Id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.UpdateWarehouse(warehouseEntity);
    }
    
    public async Task AssignOperator(FranchiseUserEntity franchiseUserEntity, Guid warehouseId)
    {
        if (!await _warehouseRepository.Exists(warehouseId))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        if(await _warehouseRepository.GetOperator(warehouseId) != null)
        {
            throw new Exception("Warehouse already has an operator");
        }
        
        await _warehouseRepository.AssignOperator(franchiseUserEntity, warehouseId);
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

        /*foreach (var warehouse in warehouses)
        {
            var newWarehouse = new WarehouseEntity(warehouse.Id, warehouse.Name, warehouse.PhoneNumber,
                warehouse.Code, warehouse.Deleted, warehouse.DefaultLanguage, warehouse.IsPayoutLockedForOtherCostCenter);
            var location = new LocationEntity(warehouse.Address, warehouse.City, warehouse.PostalCode);
            newWarehouse.Location = location;
            await _warehouseRepository.InsertWarehouse(newWarehouse);
            //insert lokacije
        }*/
        
    }
}