using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Dto;

public class ProductMoveHistoryDTO
{

    public int ProductId { get; set; }
    
    public virtual Data.Identities.Product Product { get; set; }
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
    public int Change { get; set; } // This field already indicates the difference (before and after)
    public int QuantityBeforeChange { get; set; } // New field to track quantity before the change
    public int QuantityAfterChange { get; set; } // New field to track quantity after the change
    public MoveType MoveType { get; set; }
    
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}