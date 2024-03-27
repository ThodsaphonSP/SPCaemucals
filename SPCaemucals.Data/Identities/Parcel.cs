using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class Parcel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string SaleManId { get; set; }
    public string DeliveryManId { get; set; }
    public  ParcelStatus ParcelStatus { get; set; }
    [Comment("หมายเลข tracking จากขนส่ง")]
    public string? VendorTrackingNo { get; set; }
        
    [Comment("พนักงานส่งของ ของบริษัท")]
    public virtual ApplicationUser ShippingCoordinator { get; set; }
    [Comment("เก็บเงินปลายทาง Cash on delivery")]
    public bool CashOnDelivery { get; set; }
    [Comment("sale man")] public virtual ApplicationUser SaleMan { get; set; }
    public virtual Customer Customer { get; set; }
    public DeliveryVendor DeliveryVendor { get; set; }
    public int DeliveryVendorId { get; set; }
    public virtual IEnumerable<ProductParcel>? ProductParcels { get; set; }
}

public class DeliveryVendor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Parcel> Parcels { get; set; }
}