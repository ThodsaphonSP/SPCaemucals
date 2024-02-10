using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>
        , ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductMoveHistory> ProductMoveHistories { get; set; }
        public DbSet<Company> Company { get; set; }
        public override DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureApplicationUserEntity(modelBuilder); 

            ConfigureApplicationUserRoleEntity(modelBuilder);

            SeedCompanies(modelBuilder);

            SeedApplicationUsers(modelBuilder);

            SeedApplicationRoles(modelBuilder); 

            ConfigureUserRoleDataSeeding(modelBuilder);

            ConfigureProductMoveHistoryEntity(modelBuilder);

            ConfigureProductEntity(modelBuilder);
            
        }
    
        private void ConfigureApplicationUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.RefreshTokens)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId);
        
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId);
        }

        private void ConfigureApplicationUserRoleEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
        }

        private void SeedCompanies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "S&P",
                },
                new Company
                {
                    CompanyId = 2,
                    CompanyName = "S2P",
                }, 
                new Company
                {
                    CompanyId = 3,
                    CompanyName = "Aceepta",
                });
        }

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
                CompanyId = 1
            };

            return adminUser;
        }

        private ApplicationRole GetAdminRole()
        {
            var adminRole = new ApplicationRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            return adminRole;
        }

        private void SeedApplicationUsers(ModelBuilder modelBuilder)
        {
            ApplicationUser adminUser = GetAdminUser();
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
        }

        private void SeedApplicationRoles(ModelBuilder modelBuilder)
        {
            ApplicationRole adminRole = GetAdminRole();
            modelBuilder.Entity<ApplicationRole>().HasData(adminRole);

            var customRoles = new[]
            {
                new ApplicationRole { Id = "2", Name = "SSaleRole", NormalizedName = "SSALEROLE" },
                new ApplicationRole { Id = "3", Name = "JSaleRole", NormalizedName = "JSALEROLE" },
                new ApplicationRole { Id = "4", Name = "RSaleRole", NormalizedName = "RSALEROLE" },
                new ApplicationRole { Id = "5", Name = "AccountRole", NormalizedName = "ACCOUNTROLE" }
            };

            modelBuilder.Entity<ApplicationRole>().HasData(customRoles);
        }

        private void ConfigureUserRoleDataSeeding(ModelBuilder modelBuilder)
        {
            ApplicationUser adminUser = GetAdminUser();
            ApplicationRole adminRole = GetAdminRole();

            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole
                {
                    RoleId = adminRole.Id,
                    UserId = adminUser.Id,
                });
        }

        private void ConfigureProductEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
        private void ConfigureProductMoveHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMoveHistory>()
                .HasOne(e => e.Product)
                .WithMany(e => e.ProductMoveHistories)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ProductMoveHistory>()
                .HasOne(pmh => pmh.Category)
                .WithMany(c => c.ProductMoveHistories)
                .HasForeignKey(pmh => pmh.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            
            modelBuilder.Entity<ProductMoveHistory>()
                .Property(e => e.MoveType)
                .HasConversion<int>();
        }

       
        
        
    }
}