using WebApplication1.Database.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
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
        if (await _warehouseRepository.Exists(warehouse.id))
        {
            throw new Exception("Warehouse already exists"); 
        }
        
        await _warehouseRepository.InsertWarehouse(warehouse);
    }
    
    public async Task DeleteWarehouse(Guid id)
    {
        await _warehouseRepository.DeleteWarehouse(id);
    }
    
    public async Task UpdateWarehouse(Warehouse warehouse)
    {
        if (!await _warehouseRepository.Exists(warehouse.id))
        {
            throw new Exception("Warehouse does not exist");
        }
        
        await _warehouseRepository.UpdateWarehouse(warehouse);
    }
}