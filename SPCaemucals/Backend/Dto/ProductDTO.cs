using System.ComponentModel.DataAnnotations;

namespace SPCaemucals.Backend.Dto;

public class ProductDTO
{

    public string Name { get; set; }
    public string Code { get; set; }

    public string? Detail { get; set; }
  
    
   
    public decimal? StandardPrice { get; set; }

    public decimal Multiplier { get; set; } 
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    
    
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public string CreatedById { get; set; }
    
    public DateTime UpdatedDate { get; set; }
    

    public string? UpdatedBy { get; set; }
    
    public bool IsActive { get; set; }
    
    public int UnitOfMeasurementId { get; set; }
    
    public int VendorId { get; set; }
}

