
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Dto;

public class AddressDTO
{
    public int Id { get; set; }
 
    
    public string AddressDetail { get; set; } = "";
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int SubDistrictId { get; set; }
    public int PostalCodeCodeId { get; set; }
    
    
    public virtual ProvinceDTO Province { get; set; }
    public virtual DistrictDTO District { get; set; }
    public virtual SubDistrictDTO SubDistrict { get; set; }
    public virtual PostalDTO PostalCode { get; set; }
    
    
    
    public bool Equals(Address test)
    {
        var string1 = AddressDetail.Replace(" ", "");
        var string2 = test.AddressDetail.Replace(" ", "");
        
        return string1 == string2
               && ProvinceId == test.ProvinceId
               && DistrictId == test.DistrictId
               && SubDistrictId == test.SubDistrictId
               && PostalCodeCodeId == test.PostalCodeCodeId;

    }
}