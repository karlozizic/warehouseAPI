namespace WebApplication1.Models;

public class ItemRequestDto
{
    public Guid TenantId { get; set; }
    public Guid ItemId { get; set; }
    public String ItemName { get; set; }
    public String? ItemDescription { get; set; }
    public Guid? CurrentWarehouseId { get; set; }
    public Guid RequestedWarehouseId { get; set; }
    public Guid OperatorId { get; set; }
}