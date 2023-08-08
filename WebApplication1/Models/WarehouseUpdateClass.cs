using WebApplication1.Database.Entities;

namespace WebApplication1.Models;

public class WarehouseUpdateClass
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public LocationEntity? Location { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Code {get; set; }
    public bool Deleted { get; set; }
    public string? DefaultLanguage { get; set; }
    public bool IsPayoutLockedForOtherCostCenter { get; set; }
    public FranchiseUserEntity? OperatorUser { get; set; }
    //promijeni List u IQueryable - kako bi se optimizirali upiti prema bazi podataka 
    public List<ItemEntity>? Items { get; set; }
    public Guid TenantId { get; set; }
}