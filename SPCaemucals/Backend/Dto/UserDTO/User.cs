using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Dto.CompanyDTO;
using SPCaemucals.Backend.Dto.Role;


public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string CompanyId { get; set; }
    
    // Navigation property
    public Company Company { get; set; }
    public virtual AddressDTO? Address { get; set; }
    
    public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>(); // Use UserRoleDto here
}