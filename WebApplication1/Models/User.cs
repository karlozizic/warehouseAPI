namespace WebApplication1.Models;

public class User
{
    public string id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? username { get; set; }
    public string? email { get; set; }
    public Boolean active { get; set; }
    public DateTime? createdDateTime { get; set; }
    public List<string>? grantGroups { get; set; }
    public Boolean deleted { get; set; }
}