namespace SPCaemucals.Backend.Models;

public class UserLoginRequest
{
    public string EmailOrPhone { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
