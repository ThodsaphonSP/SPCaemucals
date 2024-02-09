using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Models;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;
using SPCaemucals.Utility;

namespace SPCaemucals.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly CorrelationIdHelper _coreHelper;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager
            ,IMapper mapper, ApplicationDbContext appDbContext,IConfiguration configuration,RoleManager<ApplicationRole> roleManager
            ,ILogger<AccountController> logger,
            CorrelationIdHelper coreHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._mapper = mapper;
            _appDbContext = appDbContext;
            _configuration = configuration;
            _roleManager = roleManager;
            _logger = logger;
            _coreHelper = coreHelper;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            // The properties will be assigned once, no need to initialize them with empty strings.
            string email, phone, id, userName = model.EmailOrPhone;

            // Used conditional operators (ternary operators) to simplify the code
            var user = userName.Contains("@")
                ? await _userManager.FindByEmailAsync(userName)
                : await _userManager.Users.FirstOrDefaultAsync(user => user.PhoneNumber == userName);

            // Check if user is not null and assign necessary variables.
            if (user != null)
            {
                userName = user.UserName;
                email = user.Email;
                phone = user.PhoneNumber;
                id = user.Id;
            }
            else // If no user is found we can directly return UnAuthorized.
            {
                return Unauthorized();
            }

            // Authenticate the user.
            var result = await _signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe,
                lockoutOnFailure: false);

            // Check if the authentication succeeded.
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            int tokenMinuteDuration = Convert.ToInt32(_configuration["tokenMinuteDuration"]);
            // Setup the JWT token.
            string jwtKey = _configuration["JwtKey"];
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Email", email),
                    new Claim("Phone", phone),
                    new Claim("UserId", id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenMinuteDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(1), // Set this according to your requirements
                Created = DateTime.UtcNow,
                CreatedByIp = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = user.Id,
                User = user
            };

            // Assuming you are using an Entity Framework Core context
            // Replace 'context' with the name of your DbContext
            _appDbContext.RefreshToken.Add(refreshToken);
            await _appDbContext.SaveChangesAsync();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            // Set access token in response header
            Response.Headers.Add("Authorization", $"Bearer {tokenHandler.WriteToken(token)}");
    
            return Ok(new
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            });

           
            
        }
        
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            // Get Claim
            var claim = User.Claims.First(c => c.Type == "UserId");
            if (claim == null)
            {
                return Unauthorized();
            }

            // Retrieve user's refresh tokens
            var refreshTokens = await _appDbContext.RefreshToken.Where(t => t.UserId == claim.Value).ToListAsync();

            // Remove the tokens
            _appDbContext.RefreshToken.RemoveRange(refreshTokens);
            await _appDbContext.SaveChangesAsync();

            // SignOut from SignInManager
            await _signInManager.SignOutAsync();
            return Ok(); // Simplified, replace with actual success response
        }
        [AllowAnonymous]
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
            
            

            using (var transaction = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        CompanyId = model.CompanyId
                    };

                    var company = await _appDbContext.Company.FirstOrDefaultAsync(c => c.CompanyId == model.CompanyId);
                    if (company == null)
                    {
                        return BadRequest("Invalid CompanyId");
                    }
            
                    user.Company = company;

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest(ModelState);
                    }

                    var addRoleResult = await _userManager.AddToRolesAsync(user, model.RoleName);
                    if (!addRoleResult.Succeeded)
                    {
                        foreach (var error in addRoleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest(ModelState);
                    }
        
                    // If everything is successful, commit the transaction
                    transaction.Commit();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    
                    _logger.LogInformation("{@id} | register user {@user}",_coreHelper.GetCorrelationId(),user);
                    return Ok("User registered successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError("correlationId: {@core} | {@error}",_coreHelper.GetCorrelationId(),ex);
                    return StatusCode(500);
                    // Other error handling, e.g. return a "500 Internal Server Error" to the client
                }
            }
        }

        [HttpGet]
        [Route("info")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserInfo()
        {
            var id =  User.FindFirstValue("UserId");
            ApplicationUser? user = await _appDbContext.Users
                .Include(u => u.Company)
                .Include(x=>x.UserRoles)
                .ThenInclude(x=>x.Role).FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound("User not found.");
            }
            UserDto userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenRequest request)
        {
            var refreshToken = await _appDbContext.RefreshToken.SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (refreshToken == null)
            {
                return BadRequest("Invalid refresh token");
            }

            if (refreshToken.Expires < DateTime.UtcNow)
            {
                return BadRequest("Refresh token has expired");
            }

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if (user == null)
            {
                return BadRequest("Invalid user");
            }

            // Generate new JWT and refresh tokens
            string jwtKey = _configuration["JwtKey"];
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Email", user.Email),
                    new Claim("Phone", user.PhoneNumber),
                    new Claim("UserId", user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var newRefreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(1), // Set this according to your requirements
                Created = DateTime.UtcNow,
                CreatedByIp = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = user.Id,
                User = user
            };

            // Delete old refresh token and add new one
            _appDbContext.RefreshToken.Remove(refreshToken);
            _appDbContext.RefreshToken.Add(newRefreshToken);
            await _appDbContext.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = newRefreshToken.Token
            });
        }
        

        [HttpGet]
        [Route("Users")]
        // [ProducesResponseType(typeof(PagedList<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 10,string phoneOrMail="")
        {
            var users = _appDbContext.Users;

            if (!users.Any())
            {
                return NotFound("Users not found.");
            }

            var query = _appDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(phoneOrMail))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{phoneOrMail}%") || EF.Functions.Like(u.PhoneNumber, $"%{phoneOrMail}%"));
            }
            
            
            query = _appDbContext.Users
                .Where(u => string.IsNullOrEmpty(phoneOrMail) || EF.Functions.Like(u.Email, $"%{phoneOrMail}%") || EF.Functions.Like(u.PhoneNumber, $"%{phoneOrMail}%"))
                .OrderBy(u => u.UserName)
                .Include(u => u.Company)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);

            var pagedUsers = await PagedList<ApplicationUser>.CreateAsync(query, pageNumber, pageSize);
            

            var usersDto = _mapper.Map<List<UserDto>>(pagedUsers);

            var newOp = new
            {
                pagedUsers.TotalCount, pagedUsers.CurrentPage, pagedUsers.PageSize, pagedUsers.TotalPages,
                Users = usersDto
            };

            return Ok(newOp);
        }
        
        


    }
}
