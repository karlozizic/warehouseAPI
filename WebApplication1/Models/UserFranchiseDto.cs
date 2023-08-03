namespace WebApplication1.Models;

public class UserFranchiseDto
{
    public string id { get; set; }
    public string userId { get; set; }
    public string franchiseId { get; set; }
    public string? franchiseName { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? username { get; set; }
    public Boolean wallet { get; set; }
    public DateTime createdDateTime { get; set; }
    public List<string> grantGroups { get; set; }
    public string? email { get; set; }
    public Boolean active { get; set; }
    public Boolean deleted { get; set; }
    public List<string> costCenterIdList { get; set; }
    public string franchiseUserTableId { get; set; }
}