using SPCaemucals.Data.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPCaemucals.Data.Models;

public class Product : BaseModel
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductMoveHistory> ProductMoveHistories { get; set; }
}