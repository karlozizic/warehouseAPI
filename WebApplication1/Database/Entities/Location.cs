using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Database.Entities;

public class Location
{
    [Key]
    public Guid id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
    public String address { get; set; }
    public String city { get; set; }
    [MaxLength(10, ErrorMessage = "Postal code cannot be longer than 10 characters.")] 
    public String? postalCode { get; set; }
    
    public String? latitude { get; set; }
    
    public String? longitude { get; set; }
    
}