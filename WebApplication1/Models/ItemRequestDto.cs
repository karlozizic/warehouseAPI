namespace WebApplication1.Models;

public class ItemRequestDto
{
    public Guid ItemId { get; set; }
    public Guid WarehouseId { get; set; }
    public String ItemName { get; set; }
    public String? ItemDescription { get; set; }
    public Guid? CurrentWarehouseId { get; set; }
    public Guid RequestedWarehouseId { get; set; }
    public Guid? RequestOperatorId { get; set; }
}