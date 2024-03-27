using SPCaemucals.Backend.Controllers;

namespace SPCaemucals.Backend.Dto.Model;

public class SenderDetails
{
    
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? PhoneNo { get; set; }
    public DeliveryVendorDTO VendorDelivery { get; set; }
    public List<SelectProduct> SelectProduct { get; set; }
    
}

public class SelectProduct
{
    public ProductQuantity IndexNumber { get; set; }
}