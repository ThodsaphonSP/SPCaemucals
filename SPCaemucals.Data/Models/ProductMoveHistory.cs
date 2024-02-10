
using System.ComponentModel.DataAnnotations.Schema;
using SPCaemucals.Data.Enum;

namespace SPCaemucals.Data.Models;

public class ProductMoveHistory 
{
    [ForeignKey(nameof(ProductId))]
    public Guid ProductId { get; set; }
    
    public virtual Product Product { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    public int Change { get; set; } // This field already indicates the difference (before and after)
    public int QuantityBeforeChange { get; set; } // New field to track quantity before the change
    public int QuantityAfterChange { get; set; } // New field to track quantity after the change
    public MoveType MoveType { get; set; }
    
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public ProductMoveHistory()
    {
        CreatedDate = DateTime.Now;
        IsActive = true;
    }
}