
using System.ComponentModel.DataAnnotations.Schema;

namespace SPCaemucals.Data.Models;

public class Product 
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public Product()
    {
        CreatedDate = DateTime.Now;
        IsActive = true;
    }
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductMoveHistory> ProductMoveHistories { get; set; }

}