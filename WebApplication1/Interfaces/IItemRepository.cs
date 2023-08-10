using WebApplication1.Database.Entities;

namespace WebApplication1.Interfaces;

public interface IItemRepository
{
    Task UpdateItem(ItemEntity warehouseItemEntity);
    Task<Boolean> ExistsItem(Guid id);
    Task<ItemEntity> GetItemById(Guid id);
    Task InsertItem(ItemEntity itemEntity);
    Task DeleteItem(Guid id);
}