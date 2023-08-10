using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebApplication1.Database.Entities;

public class WarehouseEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }
    
    [ForeignKey("Location")]
    public Guid? LocationId { get; set; }
    public LocationEntity? Location { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Code {get; set; }
    public bool Deleted { get; set; }
    public string? DefaultLanguage { get; set; }
    public bool IsPayoutLockedForOtherCostCenter { get; set; }
    [Required]
    public Guid TenantId { get; set; }

    public WarehouseEntity(string name, Guid? locationId, string phoneNumber, string code, bool deleted,
        string defaultLanguage, bool isPayoutLockedForOtherCostCenter)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.LocationId = locationId;
        this.PhoneNumber = phoneNumber;
        this.Code = code;
        this.Deleted = deleted;
        this.DefaultLanguage = defaultLanguage;
        this.IsPayoutLockedForOtherCostCenter = isPayoutLockedForOtherCostCenter;
    }

}