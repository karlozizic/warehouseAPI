
namespace WebApplication1.Models;

public class CostCenterDto
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string? address { get; set; }
    public string? city { get; set; }
    public string? postalCode { get; set; }
    public string? phoneNumber { get; set; }
    public string? code {get; set; }
    public DateTime dateTimeCreatedUtc { get; set; }
    public Boolean deleted { get; set; }
    public Guid franchiseId { get; set; }
    public GpsInfo GpsInfo { get; set; }

}