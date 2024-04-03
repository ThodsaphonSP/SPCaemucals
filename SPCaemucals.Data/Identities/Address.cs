namespace SPCaemucals.Data.Identities;

public class Address: IEqualityComparer<Address>
{
    public int Id { get; set; }
 
    
    public string AddressDetail { get; set; } = "";
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int SubDistrictId { get; set; }
    public int PostalCodeCodeId { get; set; }
    

    
    public virtual Customer Customer { get; set; }
    public virtual Province Province { get; set; }
    public virtual District District { get; set; }
    public virtual SubDistrict SubDistrict { get; set; }
    public virtual PostalCode PostalCode { get; set; }
    public virtual ApplicationUser? Employee { get; set; }


    public bool Equals(Address x, Address y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(x, null))
            return false;

        if (ReferenceEquals(y, null))
            return false;

        if (x.GetType() != y.GetType())
            return false;

        return x.AddressDetail == y.AddressDetail
               && x.ProvinceId == y.ProvinceId
               && x.DistrictId == y.DistrictId
               && x.SubDistrictId == y.SubDistrictId
               && x.PostalCodeCodeId == y.PostalCodeCodeId
               && x.Customer.Equals(y.Customer)
               && x.Province.Equals(y.Province)
               && x.District.Equals(y.District)
               && x.SubDistrict.Equals(y.SubDistrict)
               && x.PostalCode.Equals(y.PostalCode);
    }

    public int GetHashCode(Address obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.AddressDetail);
        hashCode.Add(obj.ProvinceId);
        hashCode.Add(obj.DistrictId);
        hashCode.Add(obj.SubDistrictId);
        hashCode.Add(obj.PostalCodeCodeId);
        hashCode.Add(obj.Customer);
        hashCode.Add(obj.Province);
        hashCode.Add(obj.District);
        hashCode.Add(obj.SubDistrict);
        hashCode.Add(obj.PostalCode);
        return hashCode.ToHashCode();
    }
}