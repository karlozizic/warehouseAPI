using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;

namespace WebApplication1.Database.Entities;

public class ItemRequestEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid itemId { get; set; }
    public Guid warehouseId { get; set; }
    public ItemRequestEnum? status { get; set; }
    public Guid? requestOperatorId { get; set; }
    public Guid? approvalOperatorId { get; set; }
    public bool Deleted { get; set; }

    public ItemRequestEntity(Guid itemId, Guid warehouseId, ItemRequestEnum? status,
        Guid? requestOperatorId)
    {
        this.Id = Guid.NewGuid();
        this.itemId = itemId;
        this.warehouseId = warehouseId;
        this.status = status;
        this.requestOperatorId = requestOperatorId;
        this.Deleted = false;
    }
    
    

}