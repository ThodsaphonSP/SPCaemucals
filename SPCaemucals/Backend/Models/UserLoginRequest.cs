using Microsoft.Build.Framework;

namespace SPCaemucals.Backend.Models;

public class UserLoginRequest
{
    [Required] public string EmailOrPhone { get; set; }
    [Required] public string Password { get; set; }

    public bool RememberMe { get; set; }
}
