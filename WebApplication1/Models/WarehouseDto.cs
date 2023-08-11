namespace WebApplication1.Models;

public class WarehouseDto
{
    public string? Name { get; set; }
    public Guid? LocationId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Code {get; set; }
    public string? DefaultLanguage { get; set; }
    public bool? IsPayoutLockedForOtherCostCenter { get; set; }
    public Guid? TenantId { get; set; }
}