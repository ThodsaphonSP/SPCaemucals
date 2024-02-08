using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Repositories;

namespace SPCaemucals.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController( IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        
        
        [HttpGet()]
        public async Task<IActionResult> GetAllRole( string name="")
        {
            
            var roles = await _roleRepository.GetAllRolesAsync( name);

            return Ok(roles);
            
        }
    }
}
