using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Database.Entities;

public class FranchiseUser
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FranchiseId { get; set; }
    public string? FranchiseName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
}