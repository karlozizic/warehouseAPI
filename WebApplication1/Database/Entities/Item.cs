using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Database.Entities;

public class Item
{
    [Key]
    public Guid id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public String name { get; set; }
    [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
    public String? description { get; set; }
    public Guid tenantId { get; set; }
    public Boolean deleted { get; set; }

}