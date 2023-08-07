using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Database.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public Boolean Active { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public List<string>? GrantGroups { get; set; }
    public Boolean Deleted { get; set; }
}