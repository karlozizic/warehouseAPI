namespace WebApplication1.Models;

public class FranchiseUserDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FranchiseId { get; set; }
    public string? Username { get; set; }
    public Guid TenantId { get; set; }
    public Guid? WarehouseId { get; set; }
}