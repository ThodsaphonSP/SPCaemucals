using System.ComponentModel.DataAnnotations;

namespace SPCaemucals.Backend.Dto;

public class ProductDTO
{
    [ExcludeValue("string")]
    public string Name { get; set; }
    [ExcludeValue("string")]
    public string Code { get; set; }
    [ExcludeValue("string")]
    public string? Detail { get; set; }
  
    
   
    public decimal? StandardPrice { get; set; }

    public decimal Multiplier { get; set; } 
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    
    
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    [ExcludeValue("string")]
    public string CreatedById { get; set; }
    
    public DateTime UpdatedDate { get; set; }
    
    [ExcludeValue("string")]
    public string? UpdatedBy { get; set; }
    
    public bool IsActive { get; set; }
    
    public int UnitOfMeasurementId { get; set; }
    
    public int VendorId { get; set; }
}

public class ExcludeValueAttribute : ValidationAttribute
{
    private readonly string _value;

    public ExcludeValueAttribute(string value)
    {
        _value = value;
    }

    protected override ValidationResult IsValid(object input, ValidationContext validationContext)
    {
        if (_value.Equals(input?.ToString()))
        {
            return new ValidationResult($"This field cannot be {_value}.");
        }
            
        return ValidationResult.Success;
    }
}