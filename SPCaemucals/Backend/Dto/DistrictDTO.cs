namespace SPCaemucals.Backend.Controllers;

public class DistrictDTO
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    
    public int ProvinceId { get; set; }
    
    public virtual ICollection<SubDistrictDTO> SubDistricts { get; set; }
    

}