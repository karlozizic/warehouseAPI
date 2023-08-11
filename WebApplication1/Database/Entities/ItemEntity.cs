using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Database.Entities;

public class ItemEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public String Name { get; set; }
    [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
    public String? Description { get; set; }
    public Boolean Deleted { get; set; }
    public Guid WarehouseId { get; set; }
    public Boolean reserved { get; set; }
    
    public ItemEntity(Guid id, String name, String? description, Guid warehouseId)
    {
        Id = id;
        Name = name;
        Description = description;
        WarehouseId = warehouseId;
        Deleted = false;
        reserved = false;
    }
    
}