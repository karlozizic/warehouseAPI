using WebApplication1.Database.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ILocationRepository _locationRepository;
    
    public WarehouseService(IWarehouseRepository warehouseRepository,
        ILocationRepository locationRepository)
    {
        _warehouseRepository = warehouseRepository; 
        _locationRepository = locationRepository;
    }
    
    public async Task<List<WarehouseDto>> GetWarehouses(Guid tenantId)
    {
        List<WarehouseEntity> warehouses = await _warehouseRepository.GetWarehouses(tenantId);
        List<WarehouseDto> warehouseDtos = new List<WarehouseDto>();
        foreach (var warehouse in warehouses)
        {
            WarehouseDto warehouseDto = new WarehouseDto()
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                PhoneNumber = warehouse.PhoneNumber,
                Code = warehouse.Code,
                Deleted = warehouse.Deleted
            };
            if (warehouse.Location != null)
            {
                warehouseDto.Address = warehouse.Location.Address;
                warehouseDto.City = warehouse.Location.City;
                warehouseDto.PostalCode = warehouse.Location.postalCode;
            }
            warehouseDtos.Add(warehouseDto);
        }

        return warehouseDtos; 
    }
    
    public async Task<WarehouseDto> GetWarehouseById(Guid tenantId, Guid id)
    {
        if (!await _warehouseRepository.Exists(tenantId, id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        WarehouseEntity? warehouseEntity = await _warehouseRepository.GetWarehouseById(tenantId, id);
        WarehouseDto warehouseDto = new WarehouseDto()
        {
            Id = warehouseEntity.Id,
            Name = warehouseEntity.Name,
            PhoneNumber = warehouseEntity.PhoneNumber,
            Code = warehouseEntity.Code,
            Deleted = warehouseEntity.Deleted
        };
        if (warehouseEntity.Location != null)
        {
            warehouseDto.Address = warehouseEntity.Location.Address;
            warehouseDto.City = warehouseEntity.Location.City;
            warehouseDto.PostalCode = warehouseEntity.Location.postalCode;
        }

        return warehouseDto;
    }
    
    
    public async Task<WarehouseDto> InsertWarehouse(Guid tenantId, WarehouseDto warehouseDto)
    {
        if (warehouseDto.Id != Guid.Empty)
        {
            if (!await _warehouseRepository.Exists(tenantId, warehouseDto.Id))
            {
                throw new Exception("Warehouse already exists"); 
            }
        }

        WarehouseEntity warehouseEntity = new WarehouseEntity(warehouseDto.Name, null, warehouseDto.PhoneNumber,
            warehouseDto.Code, false, warehouseDto.DefaultLanguage, warehouseDto.IsPayoutLockedForOtherCostCenter,
            tenantId);
        
        Guid warehouseId = await _warehouseRepository.InsertWarehouse(tenantId, warehouseEntity);
        WarehouseDto returnDto = new WarehouseDto();
        returnDto.Id = warehouseId;
        return returnDto;
    }
    
    public async Task DeleteWarehouse(Guid tenantId, Guid id)
    {
        if (!await _warehouseRepository.Exists(tenantId, id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.DeleteWarehouse(tenantId, id);
    }
    
    public async Task UpdateWarehouse(Guid tenantId, WarehouseUpdateClass warehouseUpdateClass)
    {
        var id = warehouseUpdateClass.Id; 
        if (!await _warehouseRepository.Exists(tenantId, id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.UpdateWarehouse(tenantId, warehouseUpdateClass);
        
    }

    public async Task InsertWarehouses(Guid tenantId, List<WarehouseDto> warehouses)
    {
        List<WarehouseEntity> warehouseEntities = new List<WarehouseEntity>();

        foreach (var warehouse in warehouses)
        {
            // potrebna je efikasnija implementacija - zbog ovog duze traje request
            Guid locationId = await _locationRepository.Add(tenantId, new LocationEntity(warehouse.Address, warehouse.City, warehouse.PostalCode));
            
            warehouseEntities.Add(new WarehouseEntity(warehouse.Id, warehouse.Name, locationId, warehouse.PhoneNumber,
                warehouse.Code, warehouse.Deleted, warehouse.DefaultLanguage, warehouse.IsPayoutLockedForOtherCostCenter, tenantId));

        }
        
        await _warehouseRepository.InsertAllWarehouses(tenantId, warehouseEntities);

    }
}

public interface IWarehouseService
{
    Task<List<WarehouseDto>> GetWarehouses(Guid tenantId);
    Task<WarehouseDto> GetWarehouseById(Guid tenantId, Guid id);
    Task<WarehouseDto> InsertWarehouse(Guid tenantId, WarehouseDto warehouseEntity);
    Task DeleteWarehouse(Guid tenantId, Guid id);
    Task UpdateWarehouse(Guid tenantId, WarehouseUpdateClass warehouseEntity);
    
    /*
    Task<List<ItemEntity>> GetWarehouseItems(Guid warehouseId, String? name);
    */
    
    Task InsertWarehouses(Guid tenantId, List<WarehouseDto> warehouses);
}