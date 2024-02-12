using System.ComponentModel.DataAnnotations;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string PhoneNo { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }
        
    public virtual ICollection<Parcel> Parcels { get; set; }
}