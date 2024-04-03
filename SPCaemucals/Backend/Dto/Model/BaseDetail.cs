using SPCaemucals.Backend.Controllers;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Dto.Model;

public class BaseDetail
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNo { get; set; }
    public string AddressText { get; set; }
    public ProvinceDTO Province { get; set; }
    public DistrictDTO District { get; set; }
    public SubDistrictDTO SubDistrict { get; set; }
    
    public Address GetAddress()
    {

        Address address = new Address();
        
        address.AddressDetail = AddressText;
        address.ProvinceId = Province.Id;
        address.DistrictId = District.Id;
        address.SubDistrictId = SubDistrict.Id;
        address.PostalCodeCodeId = PostalCode.Id;
        return address;
        
    }

        

    public PostalDTO PostalCode { get; set; }
   
    public bool SaveAddress { get; set; }
       
}