using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class FranchiseDto
{
    public string id { get; set; }
    public string? name { get; set; }
    public string? companyName { get; set; }
    public string? location { get; set; }
    public Boolean isTest { get; set; }
    public string? logo { get; set; }
    public string? alternateLogo { get; set; }
    public string? shortCode { get; set; }
    public Boolean printTicketPayoutConfirmation { get; set; }
    public Boolean ignoreLinkedCostCenterLimit { get; set; }
    public string? backgroundImage { get; set; }
    public Double creditTicketAmountLimit { get; set; }
    

}