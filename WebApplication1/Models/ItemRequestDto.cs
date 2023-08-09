namespace WebApplication1.Models;

public class ItemRequestDto
{
    public Guid itemId { get; set; }
    public Guid warehouseId { get; set; }
    public Guid operatorId { get; set; }
}