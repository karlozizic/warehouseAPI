
namespace WebApplication1.Models;

public class CostCenterDto
{
    public string id { get; set; }
    public string name { get; set; }
    public string? address { get; set; }
    public string? city { get; set; }
    public string? postalCode { get; set; }
    public string? phoneNumber { get; set; }
    public string? code {get; set; }
    public DateTime dateTimeCreatedUtc { get; set; }
    public Boolean deleted { get; set; }
    public string franchiseId { get; set; }
    public FranchiseDto franchise { get; set; }
    //terminals - List<TerminalBaseDto>
    //comments - List<CommmentDto>
    //linkedCostCenters - List<CostCenterHeader>
    public GpsInfo GpsInfo { get; set; }
    public string defaultLanguage { get; set; }
    //tags - List<TagDetails>
    public DateTime? dateOpenUtc { get; set; }
    public DateTime? dateClosedUtc { get; set; }
    public Boolean isPayoutLockedForOtherCostCenter { get; set; }
    //workdates - List<WorkDate>
    //depositLimit 

}