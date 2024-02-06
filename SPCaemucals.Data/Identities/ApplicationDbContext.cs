using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductMoveHistory> ProductMoveHistories { get; set; }
    public DbSet<Company> Company { get; set; }
    
    
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
        
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Company)  // User has one Company
            .WithMany(c => c.Users)  // Company has many Users
            .HasForeignKey(u => u.CompanyId);  // Foreign key
        
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
        
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "1",
                UserName = "user",
                FirstName = "John",
                                LastName = "Doe",
                NormalizedUserName = "USER",
                PhoneNumber = "0918131501",
                Email = "user@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Password"),
                SecurityStamp = string.Empty,
                CompanyId = 1
            });
        
        
        
        modelBuilder.Entity<ProductMoveHistory>()
            .HasOne(e => e.Product)
            .WithMany(e => e.ProductMoveHistories)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }
}