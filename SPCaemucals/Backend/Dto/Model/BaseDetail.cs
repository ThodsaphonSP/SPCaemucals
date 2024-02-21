using SPCaemucals.Backend.Controllers;

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

        

    public PostalDTO PostalCode { get; set; }
    public DeliveryVendorDTO VendorDelivery { get; set; }
    public bool SaveAddress { get; set; }
       
}