namespace SPCaemucals.Backend.Controllers;

public class SubDistrictDTO
{
    public int Id { get; set; }
    public string ThaiName { get; set; }
    public int DistrictId { get; set; }
    
    public virtual ICollection<PostalDTO> PostalCodes { get; set; }
}