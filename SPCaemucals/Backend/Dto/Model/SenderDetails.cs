using SPCaemucals.Backend.Controllers;

namespace SPCaemucals.Backend.Dto.Model;

public class SenderDetails:BaseDetail
{
    public DeliveryVendorDTO VendorDelivery { get; set; }
    public List<SelectProduct> SelectProduct { get; set; }
    
}

public class SelectProduct
{
    public ProductQuantity IndexNumber { get; set; }
}