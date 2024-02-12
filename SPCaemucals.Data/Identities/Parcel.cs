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
        
    public virtual ApplicationUser DeliveryMan { get; set; }
    public virtual ApplicationUser SaleMan { get; set; }
    public virtual Customer Customer { get; set; }

}