using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Data.Models;

public class ApplicationUser:IdentityUser
{
    [Required]
    public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    
    // Foreign key
    [Required]
    [ForeignKey("Company")]

    public int CompanyId { get; set; }

    public int? AddressId { get; set; }

    // Navigation property
    public Company Company { get; set; }
    
    //Navigation propery

    public virtual Address? Address { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } 
    public List<RefreshToken> RefreshTokens { get; set; }
    
    public virtual ICollection<Parcel> SoldItem { get; set; }
    public virtual ICollection<Parcel> ShippedPackage { get; set; }
    
  
}

public class ApplicationUserRole:IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}

public class ApplicationRole : IdentityRole
{
    // ... your other properties

    // Navigation property for associated Users
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}