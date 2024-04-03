using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Repositories;

public interface IRoleRepository
{
    Task<ApplicationRole?> GetRoleByIdAsync(string roleId);
    Task<ApplicationRole?> GetRoleByNameAsync(string roleName);
    Task<List<ApplicationRole>> GetAllRolesAsync(string name);
    Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName);
    Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName);
    Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName);
    Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName);
}

public class RoleRepository : IRoleRepository
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ApplicationRole?> GetRoleByIdAsync(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId);
    }

    public async Task<ApplicationRole?> GetRoleByNameAsync(string roleName)
    {
        return await _roleManager.FindByNameAsync(roleName);
    }

    public async Task<List<ApplicationRole>> GetAllRolesAsync( string name)
    {
        var query = _roleManager.Roles.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(u => EF.Functions.Like(u.Name, $"%{name}%") );
        }

        query = query.OrderBy(x => x.Name);

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName)
    {
        IList<ApplicationUser> users = await _userManager.GetUsersInRoleAsync(roleName);
        return users.ToList();
    }

    public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName)
    {
        return await _userManager.RemoveFromRoleAsync(user, roleName);
    }
}