namespace SPCaemucals.Data.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public virtual Address Address { get; set; }

    public int AddressId { get; set; }

    // Collection of User entities
    public ICollection<ApplicationUser> Users { get; set; }
}