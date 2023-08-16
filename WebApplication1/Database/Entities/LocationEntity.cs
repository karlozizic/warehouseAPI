using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Database.Entities;

[ComplexType]
public class LocationEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
    public string Address { get; set; }
    public string City { get; set; }
    [MaxLength(10, ErrorMessage = "Postal code cannot be longer than 10 characters.")] 
    public string? postalCode { get; set; }
    
    public string? Latitude { get; set; }
    
    public string? Longitude { get; set; }
    
    public LocationEntity(string address, string city, string? postalCode)
    {
        Id = Guid.NewGuid();
        this.Address = address;
        this.City = city;
        this.postalCode = postalCode;
    }

    public LocationEntity()
    {
    }

}