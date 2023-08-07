using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IItemService
{
    Task UpdateItem(ItemEntity warehouseItemEntity);
}