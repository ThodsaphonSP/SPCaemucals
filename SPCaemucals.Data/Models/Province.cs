

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPCaemucals.Data.Models;

public class Province
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    public virtual ICollection<District> Districts { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }
}


public class District
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    
    public int ProvinceId { get; set; }
    
    public virtual Province Province { get; set; }
    public virtual ICollection<SubDistrict> SubDistricts { get; set; }
    
    public virtual ICollection<Address> Addresses { get; set; }
}

public class SubDistrict
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    public int DistrictId { get; set; }
    public virtual District District { get; set; }
    public virtual ICollection<PostalCode> PostalCodes { get; set; }
    
    public virtual ICollection<Address> Addresses { get; set; }
}

public class PostalCode
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    public int SubDistrictId { get; set; }
    public virtual SubDistrict SubDistrict { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }
}