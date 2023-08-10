using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;

namespace WebApplication1.Database.Entities;

public class ItemRequestEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public String ItemName { get; set; }
    public String? ItemDescription { get; set; }
    public Guid? CurrentWarehouseId { get; set; }
    public Guid RequestedWarehouseId { get; set; }
    public ItemRequestEnum? Status { get; set; }
    public Guid? RequestOperatorId { get; set; }
    public Guid? ApprovalOperatorId { get; set; }
    public bool Deleted { get; set; }

    public ItemRequestEntity(Guid itemId, String itemName, String? itemDescription, Guid? currentWarehouseId,
        Guid requestedWarehouseId, Guid? requestOperatorId)
    {
        this.Id = Guid.NewGuid();
        this.ItemId = itemId;
        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.CurrentWarehouseId = currentWarehouseId;
        this.RequestOperatorId = requestOperatorId;
        this.Deleted = false;
    }
    
    

}