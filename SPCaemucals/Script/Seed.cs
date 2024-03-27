using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

// Ensure this is the correct namespace for your models

// Ensure this is the correct namespace


namespace SPCaemucals.Script;

public class Seed
{
    private readonly ApplicationDbContext _context;


    public Seed(ApplicationDbContext context)
    {
        _context = context;
    }

    

    public Company Company { get; set; }

    public async Task InsertProvinceAsync(string filePath)
    {
        if (_context.Districts.Any())
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        string sqlCommand = await File.ReadAllTextAsync(filePath);


        await _context.Database.ExecuteSqlRawAsync(sqlCommand);
    }


  


    // public async Task SeedCompany()
    // {
    //     var company = new Company()
    //     {
    //         CompanyName = "s&p",
    //         Address = this.Address
    //     };
    //     _context.Company.Add(company);
    //     await _context.SaveChangesAsync();
    //
    //     this.Company = company;
    // }

   


   

    

    private ApplicationUser GetAdminUser()
    {
        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

        var adminUser = new ApplicationUser
        {
            Id = "1",
            UserName = "S&P_01",
            FirstName = "John",
            LastName = "Doe",
            NormalizedUserName = "S&P_01",
            PhoneNumber = "0918131505",
            Email = "admin@sw.com",
            NormalizedEmail = "ADMIN@SW.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "pass@word"),
            SecurityStamp = string.Empty,
            Company = Company
        };

        return adminUser;
    }

    public async Task SeedApplicationUsers()
    {
        ApplicationUser adminUser = GetAdminUser();

        _context.Users.Add(adminUser);

        this.Admin = adminUser;

        await _context.SaveChangesAsync();

        await SeedApplicationRoles();

        var rolemap = new ApplicationUserRole
        {
            RoleId = this.Roles[0].Id,
            UserId = this.Admin.Id,
        };

        _context.UserRoles.Add(rolemap);

        await _context.SaveChangesAsync();
    }

    public ApplicationUser Admin { get; set; }


    public async Task SeedApplicationRoles()
    {
        var customRoles = new[]
        {
            new ApplicationRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new ApplicationRole { Id = "2", Name = "SSaleRole", NormalizedName = "SSALEROLE" },
            new ApplicationRole { Id = "3", Name = "JSaleRole", NormalizedName = "JSALEROLE" },
            new ApplicationRole { Id = "4", Name = "RSaleRole", NormalizedName = "RSALEROLE" },
            new ApplicationRole { Id = "5", Name = "AccountRole", NormalizedName = "ACCOUNTROLE" },
            new ApplicationRole()
                { Id = "6", Name = "ShippingCoordinatorRole", NormalizedName = "ShippingCoordinatorRole".ToUpper() }
        };

        await _context.Roles.AddRangeAsync(customRoles);

        await _context.SaveChangesAsync();

        this.Roles = customRoles;
    }

    public ApplicationRole[] Roles { get; set; }
}