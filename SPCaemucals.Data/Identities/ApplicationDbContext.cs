using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductMoveHistory> ProductMoveHistories { get; set; }
    public DbSet<Company> Company { get; set; }
    
    // Define DbSet for Role
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }
    
    
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.RefreshTokens)
            .WithOne(e => e.User)
            .HasForeignKey(uc => uc.UserId);
        
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Company)  // User has one Company
            .WithMany(c => c.Users)  // Company has many Users
            .HasForeignKey(u => u.CompanyId);  // Foreign key
        
        
        modelBuilder.Entity<ApplicationUserRole>(userRole =>
        {
            userRole.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        });
        
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                CompanyId = 1,
                CompanyName = "S&P",

                // Include Users as required
            },
            new Company
            {
                CompanyId = 2,
                CompanyName = "S2P",
                // Include Users as required
            }, 
            new Company
            {
                CompanyId = 3,
                CompanyName = "Aceepta",
                // Include Users as required
            });
        
        
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
            CompanyId = 1
        };
        
        modelBuilder.Entity<ApplicationUser>().HasData( adminUser);
        
        var adminRole = new ApplicationRole() 
        { 
            Id = "1", 
            Name = "Admin", 
            NormalizedName = "ADMIN"
        };
        
        
        var SSaleRole = new ApplicationRole() 
        { 
            Id = "2", 
            Name = "SSaleRole", 
            NormalizedName = "SSALEROLE"
        };
        var JSaleRole = new ApplicationRole() 
        { 
            Id = "3", 
            Name = "JSaleRole", 
            NormalizedName = "JSALEROLE"
        };
        
        var RSaleRole = new ApplicationRole() 
        { 
            Id = "4", 
            Name = "RSaleRole", 
            NormalizedName = "RSALEROLE"
        };
        
        
        var AccountRole = new ApplicationRole() 
        { 
            Id = "5", 
            Name = "AccountRole", 
            NormalizedName = "ACCOUNTROLE"
        };
        
        // Define role
        modelBuilder.Entity<ApplicationRole>().HasData(adminRole);
        
        modelBuilder.Entity<ApplicationRole>().HasData(SSaleRole);
        modelBuilder.Entity<ApplicationRole>().HasData(JSaleRole);
        modelBuilder.Entity<ApplicationRole>().HasData(RSaleRole);
        modelBuilder.Entity<ApplicationRole>().HasData(AccountRole);

        modelBuilder.Entity<ApplicationUserRole>().HasData(
            new ApplicationUserRole
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id,
            });

        
        modelBuilder.Entity<ProductMoveHistory>()
            .HasOne(e => e.Product)
            .WithMany(e => e.ProductMoveHistories)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        
        
        
        

        
    }
}