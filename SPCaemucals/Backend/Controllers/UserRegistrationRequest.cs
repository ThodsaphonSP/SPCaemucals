using System.ComponentModel.DataAnnotations;

namespace SPCaemucals.Backend.Controllers;

[RegistrationValidate]
public class UserRegistrationRequest
{
    [EmailAddress]public string Email { get; set; }
    [Phone] public string PhoneNumber { get; set; }
    [Microsoft.Build.Framework.Required]
    public string FirstName { get; set; }
    [Microsoft.Build.Framework.Required]
    public string LastName { get; set; }

    public string Password { get; set; }
        
    [Microsoft.Build.Framework.Required] public int CompanyId { get; set; }
    [Microsoft.Build.Framework.Required] public List<string> RoleName { get; set; }
    // Add any additional fields as necessary
}