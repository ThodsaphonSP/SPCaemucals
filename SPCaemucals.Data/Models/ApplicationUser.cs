using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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

    // Navigation property
    public Company Company { get; set; }
}