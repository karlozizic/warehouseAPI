﻿using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using CostCenterDto = X.Retail.Shared.Models.Models.Dtos.CostCenterDto;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ILocationRepository _locationRepository;
    
    public WarehouseService(IWarehouseRepository warehouseRepository, IFranchiseUserRepository franchiseUserRepository,
        ILocationRepository locationRepository)
    {
        _warehouseRepository = warehouseRepository; 
        _locationRepository = locationRepository;
    }
    
    public async Task<List<WarehouseEntity>> GetWarehouses(Guid tenantId)
    {
        return await _warehouseRepository.GetWarehouses(tenantId);
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

    /*public async Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name)
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
    }*/
    
    public async Task InsertWarehouses(List<CostCenterDto> warehouses)
    {
        List<WarehouseEntity> warehouseEntities = new List<WarehouseEntity>();

        foreach (var warehouse in warehouses)
        {
            // potrebna je efikasnija implementacija - zbog ovog duze traje request
            Guid locationId = await _locationRepository.Add(new LocationEntity(warehouse.Address, warehouse.City, warehouse.PostalCode));
            
            warehouseEntities.Add(new WarehouseEntity(warehouse.Name, locationId, warehouse.PhoneNumber,
                warehouse.Code, warehouse.Deleted, warehouse.DefaultLanguage, warehouse.IsPayoutLockedForOtherCostCenter));
        }
        
        await _warehouseRepository.InsertAllWarehouses(warehouseEntities);

    }
}