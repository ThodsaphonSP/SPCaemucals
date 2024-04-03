using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Dto;

public class ParcelDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string SaleManId { get; set; }
    public string DeliveryManId { get; set; }
    public  ParcelStatus ParcelStatus { get; set; }
    public string ParcelStatusName { get; set; }
    
    public string VendorTrackingNo { get; set; }
        
 
    public virtual UserDto ShippingCoordinator { get; set; }

    public bool CashOnDelivery { get; set; } 
    public virtual UserDto SaleMan { get; set; }
    public virtual CustmerDTO Customer { get; set; }
    public DeliveryVendorDTO DeliveryVendor { get; set; }
    public int DeliveryVendorId { get; set; }
    public virtual IEnumerable<ProductParcelDTO>? ProductParcels { get; set; }
   
}