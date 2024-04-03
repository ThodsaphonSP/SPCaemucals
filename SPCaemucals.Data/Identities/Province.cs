using System.ComponentModel.DataAnnotations;

namespace SPCaemucals.Data.Identities;

public class Province:IEqualityComparer<Province>
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    public virtual ICollection<District> Districts { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }

    public bool Equals(Province x, Province y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.ThaiName == y.ThaiName;
    }

    public int GetHashCode(Province obj)
    {
        return obj.ThaiName.GetHashCode();
    }
}


public class District:IEqualityComparer<District>
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    
    public int ProvinceId { get; set; }
    
    public virtual Province Province { get; set; }
    public virtual ICollection<SubDistrict> SubDistricts { get; set; }
    
    public virtual ICollection<Address> Addresses { get; set; }

    public bool Equals(District x, District y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.ThaiName == y.ThaiName ;
    }

    public int GetHashCode(District obj)
    {
        return HashCode.Combine(obj.ThaiName, obj.ProvinceId);
    }
}

public class SubDistrict:IEqualityComparer<SubDistrict>
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    public int DistrictId { get; set; }
    public virtual District District { get; set; }
    public virtual ICollection<PostalCode> PostalCodes { get; set; }
    
    public virtual ICollection<Address> Addresses { get; set; }

    public bool Equals(SubDistrict x, SubDistrict y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.ThaiName == y.ThaiName;
    }

    public int GetHashCode(SubDistrict obj)
    {
        return obj.ThaiName.GetHashCode();
    }
}

public class PostalCode:IEqualityComparer<PostalCode>
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    public int SubDistrictId { get; set; }
    public virtual SubDistrict SubDistrict { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }

    public bool Equals(PostalCode x, PostalCode y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Code == y.Code;
    }

    public int GetHashCode(PostalCode obj)
    {
        return obj.Code.GetHashCode();
    }
}