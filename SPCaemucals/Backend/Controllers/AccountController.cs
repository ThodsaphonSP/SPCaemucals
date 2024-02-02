using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Models;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            var userName = model.EmailOrPhone;

            // Determine if input is an email or a phone number
            if (userName.Contains("@")) // Simple check for email
            {
                var userByEmail = await _userManager.FindByEmailAsync(userName);
                if (userByEmail != null)
                {
                    userName = userByEmail.UserName;
                }
            }
            else // Assume it's a phone number
            {
                var userByPhone = await _userManager.Users.FirstOrDefaultAsync(user => user.PhoneNumber == userName);
                if (userByPhone != null)
                {
                    userName = userByPhone.UserName;
                }
            }

            // Attempt to sign in
            var result = await _signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Handle successful login (e.g., generate JWT token)
                return Ok(); // Simplified, replace with actual success response
            }
            else
            {
                return Unauthorized(); // Simplified, replace with actual error handling
            }
        }
        
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(); // Simplified, replace with actual success response
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest model)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the email or phone number already exists
            var userByEmail = await _userManager.FindByEmailAsync(model.Email);
            var userByPhone = await _userManager.Users.FirstOrDefaultAsync(user => user.PhoneNumber == model.PhoneNumber);

            if (userByEmail != null || userByPhone != null)
            {
                return BadRequest("Email or phone number already exists.");
            }

            // Create a new user object
            var user = new IdentityUser
            {
                UserName = model.Email, // Using email as the username
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            // Attempt to create the user with the provided password
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Optionally, sign in the user immediately or send an email confirmation
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("User registered successfully.");
            }
            else
            {
                // If there are any errors, return them
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("User not found.");
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == userName || u.PhoneNumber == userName);

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("Email or phone number already exists.");
        }


    }
    
    
    

    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        // Add any additional fields as necessary
    }
}
