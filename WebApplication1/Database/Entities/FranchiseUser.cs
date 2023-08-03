using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Database.Entities;

public class FranchiseUser
{
    [Key]
    public Guid id { get; set; }
    public Guid userId { get; set; }
    public Guid franchiseId { get; set; }
    public string? franchiseName { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? username { get; set; }
}