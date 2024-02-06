namespace SPCaemucals.Backend.Controllers;

[RequireEmailOrPhone]
public class UserRegistrationRequest
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
        
    [Microsoft.Build.Framework.Required] public int CompanyId { get; set; }
    // Add any additional fields as necessary
}