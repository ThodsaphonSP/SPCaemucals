
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPCaemucals.Data.Enum;

namespace SPCaemucals.Data.Models;

public class Product 
{
    public Product()
    {
        CreatedDate = DateTime.Now;
        IsActive = true;
    }
    public string Name { get; set; }
    public string Code { get; set; }
    
    public string? Detail { get; set; }
    public UnitOfMeasurement UnitOfMeasurement { get; set; }
    
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? StandardPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")] public decimal Multiplier { get; set; } 
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public Product? SubstituteProduct { get; set; }
    public Vendor Vendor { get; set; }
    

    
    
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductMoveHistory> ProductMoveHistories { get; set; }
    public int UnitOfMeasurementId { get; set; }
    public int VendorId { get; set; }
}

public class Vendor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<Product> Product { get; set; }
}

public class UnitOfMeasurement
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<Product>? Product { get; set; }
}