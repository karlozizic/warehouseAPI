namespace WebApplication1.Models;

public class ItemDto
{
    public String? Name { get; set; }
    public String? Description { get; set; }
    public Guid WarehouseId { get; set; }
    public Boolean reserved { get; set; }
}