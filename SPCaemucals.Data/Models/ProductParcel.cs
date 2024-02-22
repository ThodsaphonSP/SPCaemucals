using SPCaemucals.Data.Identities;

namespace SPCaemucals.Data.Models;

public class ProductParcel: IEqualityComparer<ProductParcel>
{
    public int Id { get; set; }
    
    public int Quantity { get; set; }
    public virtual Product? Product { get; set; }
    public int ProductId { get; set; }
    public virtual Parcel? Parcel { get; set; }
    public int ParcelId { get; set; }
    
    public bool Equals(ProductParcel x, ProductParcel y) 
    {
        // If ProductId and ParcelId are unique identifiers for a ProductParcel, you can use these for comparison
        if(Object.ReferenceEquals(x, y)) return true;

        if(Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;

        return x.ProductId == y.ProductId && x.ParcelId == y.ParcelId;
    }

    public int GetHashCode(ProductParcel productParcel) 
    {
        if(Object.ReferenceEquals(productParcel, null)) return 0;

        int hashProductId = productParcel.Product == null ? 0 : productParcel.ProductId.GetHashCode();
        int hashParcelId = productParcel.Parcel == null ? 0 : productParcel.ParcelId.GetHashCode();

        return hashProductId ^ hashParcelId;
    }
}