using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Database.Entities;

public class FranchiseUserEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FranchiseId { get; set; }
    public string? Username { get; set; }
    public Guid TenantId { get; set; }
    [ForeignKey("Warehouse")]
    public Guid? WarehouseId { get; set; }
    public WarehouseEntity? Warehouse { get; set; }
    
    public FranchiseUserEntity(Guid id, Guid userId, Guid franchiseId, string? username, Guid tenantId, Guid? warehouseId)
    {
        Id = id;
        UserId = userId;
        FranchiseId = franchiseId;
        Username = username;
        TenantId = tenantId;
        WarehouseId = warehouseId;
    }
}