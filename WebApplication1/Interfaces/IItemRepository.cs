using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IItemRepository
{
    Task UpdateItem(Item warehouseItem);
    Task<Boolean> ExistsItem(Guid id);
}