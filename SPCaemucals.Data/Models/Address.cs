using SPCaemucals.Data.Identities;

namespace SPCaemucals.Data.Models;

public class Address
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
}