using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Database.Entities;

public class Warehouse
{
    [Key]
    public Guid id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public String name { get; set; }
    [Required]
    public Location location { get; set; }
    //promijeni List u IQueryable - kako bi se optimizirali upiti prema bazi podataka 
    public List<Item>? items { get; set; }
    [Phone(ErrorMessage = "Invalid phone number.")]
    public String? phoneNumber { get; set; }
    public String? code {get; set; }
    public DateTime dateTimeCreatedUtc { get; set; }
    public Boolean? deleted { get; set; }
    //tenant Id 
    [Required]
    public Guid tenantId { get; set; }
    public string defaultLanguage { get; set; }
    public DateTime? dateOpenUtc { get; set; }
    public DateTime? dateClosedUtc { get; set; }
    public Boolean? isPayoutLockedForOtherCostCenter { get; set; }
    public FranchiseUser? OperatorUser { get; set; }
    
}