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
    
    public async Task<List<Warehouse>> GetWarehouses()
    {
        return await _warehouseRepository.GetWarehouses();
    }
    
    public async Task<Warehouse> GetWarehouseById(Guid id)
    {
        return await _warehouseRepository.GetWarehouseById(id);
    }
    
    
    public async Task InsertWarehouse(Warehouse warehouse)
    {
        /*if (await _warehouseRepository.Exists(warehouse.Id))
        {
            throw new Exception("Warehouse already exists"); 
        }*/
        
        await _warehouseRepository.InsertWarehouse(warehouse);
    }
    
    public async Task DeleteWarehouse(Guid id)
    {
        if (!await _warehouseRepository.Exists(id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.DeleteWarehouse(id);
    }
    
    public async Task UpdateWarehouse(Warehouse warehouse)
    {
        if (!await _warehouseRepository.Exists(warehouse.Id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.UpdateWarehouse(warehouse);
    }
    
    public async Task AssignOperator(FranchiseUser franchiseUser, Guid warehouseId)
    {
        if (!await _warehouseRepository.Exists(warehouseId))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        if(await _warehouseRepository.GetOperator(warehouseId) != null)
        {
            throw new Exception("Warehouse already has an operator");
        }
        
        await _warehouseRepository.AssignOperator(franchiseUser, warehouseId);
    }
    
    public async Task<List<Item>> GetWarehouseItems(Guid warehouseId, String? name)
    {
        if (!await _warehouseRepository.Exists(warehouseId))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        List<Item> items = await _warehouseRepository.GetWarehouseItems(warehouseId);

        if (name != null)
        {
            List<Item> filteredItems = items.Where(item => item.Name == name).ToList();
            return filteredItems;
        }

        return items; 
    }
    
    public async Task InsertWarehouses(List<CostCenterDto> warehouses)
    {
        foreach (var warehouse in warehouses)
        {
            if (await _warehouseRepository.Exists(warehouse.Id))
            {
                throw new Exception("Warehouse already exists"); 
            }
            //dodati provjeru postoji li vec lokacija
            var location = new Location(warehouse.Address, warehouse.City, warehouse.PostalCode, warehouse.GpsInfo.Latitude, warehouse.GpsInfo.Longitude); 
            /*var newWarehouse = new Warehouse(warehouse.Id, warehouse.Name, location, warehouse.PhoneNumber,
                warehouse.Code, warehouse.DateTimeCreatedUtc, warehouse.Deleted, warehouse.DefaultLanguage,
                warehouse.DateOpenUtc, warehouse.DateClosedUtc, warehouse.IsPayoutLockedForOtherCostCenter); */
            
            /*
            await _warehouseRepository.InsertWarehouse(newWarehouse);
        */
        }
        
    }
}