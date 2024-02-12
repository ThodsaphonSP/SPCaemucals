using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>
        , ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductMoveHistory> ProductMoveHistories { get; set; }
        public DbSet<Company> Company { get; set; }
        public override DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<SubDistrict> SubDistricts { get; set; }
        
        public DbSet<PostalCode> PostalCodes { get; set; }
        
        public DbSet<Address> Addresses { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        
        
        
        
        
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
            ConfigureCategoryEntity(modelBuilder);
            ConfigureProductEntity(modelBuilder);

            ConfigProvince(modelBuilder);
            ConfigDistrict(modelBuilder);
            ConfigSubDistrict(modelBuilder);
            ConfigPostalCode(modelBuilder);

            ConfigAddress(modelBuilder);

            SeedProvinces(modelBuilder);
            
            ConfigCustomer(modelBuilder);

            ConfigParcel(modelBuilder);
        }

        private void SeedProvinces(ModelBuilder modelBuilder)
        {
            List<Province> provinces = new List<Province>()
            {
                new Province { Id = 10, ThaiName = "กรุงเทพมหานคร" },
            new Province { Id = 11, ThaiName = "สมุทรปราการ" },
            new Province { Id = 12, ThaiName = "นนทบุรี" },
            new Province { Id = 13, ThaiName = "ปทุมธานี" },
            new Province { Id = 14, ThaiName = "พระนครศรีอยุธยา" },
            new Province { Id = 15, ThaiName = "อ่างทอง" },
            new Province { Id = 16, ThaiName = "ลพบุรี" },
            new Province { Id = 17, ThaiName = "สิงห์บุรี" },
            new Province { Id = 18, ThaiName = "ชัยนาท" },
            new Province { Id = 19, ThaiName = "สระบุรี" },
            new Province { Id = 20, ThaiName = "ชลบุรี" },
            new Province { Id = 21, ThaiName = "ระยอง" },
            new Province { Id = 22, ThaiName = "จันทบุรี" },
            new Province { Id = 23, ThaiName = "ตราด" },
            new Province { Id = 24, ThaiName = "ฉะเชิงเทรา" },
            new Province { Id = 25, ThaiName = "ปราจีนบุรี" },
            new Province { Id = 26, ThaiName = "นครนายก" },
            new Province { Id = 27, ThaiName = "สระแก้ว" },
            new Province { Id = 30, ThaiName = "นครราชสีมา" },
            new Province { Id = 31, ThaiName = "บุรีรัมย์" },
            new Province { Id = 32, ThaiName = "สุรินทร์" },
            new Province { Id = 33, ThaiName = "ศรีสะเกษ" },
            new Province { Id = 34, ThaiName = "อุบลราชธานี" },
            new Province { Id = 35, ThaiName = "ยโสธร" },
            new Province { Id = 36, ThaiName = "ชัยภูมิ" },
            new Province { Id = 37, ThaiName = "อำนาจเจริญ" },
            new Province { Id = 38, ThaiName = "บึงกาฬ" },
            new Province { Id = 39, ThaiName = "หนองบัวลำภู" },
            new Province { Id = 40, ThaiName = "ขอนแก่น" },
            new Province { Id = 41, ThaiName = "อุดรธานี" },
            new Province { Id = 42, ThaiName = "เลย" },
            new Province { Id = 43, ThaiName = "หนองคาย" },
            new Province { Id = 44, ThaiName = "มหาสารคาม" },
            new Province { Id = 45, ThaiName = "ร้อยเอ็ด" },
            new Province { Id = 46, ThaiName = "กาฬสินธุ์" },
            new Province { Id = 47, ThaiName = "สกลนคร" },
            new Province { Id = 48, ThaiName = "นครพนม" },
            new Province { Id = 49, ThaiName = "มุกดาหาร" },
            new Province { Id = 50, ThaiName = "เชียงใหม่" },
            new Province { Id = 51, ThaiName = "ลำพูน" },
            new Province { Id = 52, ThaiName = "ลำปาง" },
            new Province { Id = 53, ThaiName = "อุตรดิตถ์" },
            new Province { Id = 54, ThaiName = "แพร่" },
            new Province { Id = 55, ThaiName = "น่าน" },
            new Province { Id = 56, ThaiName = "พะเยา" },
            new Province { Id = 57, ThaiName = "เชียงราย" },
            new Province { Id = 58, ThaiName = "แม่ฮ่องสอน" },
            new Province { Id = 60, ThaiName = "นครสวรรค์" },
            new Province { Id = 61, ThaiName = "อุทัยธานี" },
            new Province { Id = 62, ThaiName = "กำแพงเพชร" },
            new Province { Id = 63, ThaiName = "ตาก" },
            new Province { Id = 64, ThaiName = "สุโขทัย" },
            new Province { Id = 65, ThaiName = "พิษณุโลก" },
            new Province { Id = 66, ThaiName = "พิจิตร" },
            new Province { Id = 67, ThaiName = "เพชรบูรณ์" },
            new Province { Id = 70, ThaiName = "ราชบุรี" },
            new Province { Id = 71, ThaiName = "กาญจนบุรี" },
            new Province { Id = 72, ThaiName = "สุพรรณบุรี" },
            new Province { Id = 73, ThaiName = "นครปฐม" },
            new Province { Id = 74, ThaiName = "สมุทรสาคร" },
            new Province { Id = 75, ThaiName = "สมุทรสงคราม" },
            new Province { Id = 76, ThaiName = "เพชรบุรี" },
            new Province { Id = 77, ThaiName = "ประจวบคีรีขันธ์" },
            new Province { Id = 80, ThaiName = "นครศรีธรรมราช" },
            new Province { Id = 81, ThaiName = "กระบี่" },
            new Province { Id = 82, ThaiName = "พังงา" },
            new Province { Id = 83, ThaiName = "ภูเก็ต" },
            new Province { Id = 84, ThaiName = "สุราษฎร์ธานี" },
            new Province { Id = 85, ThaiName = "ระนอง" },
            new Province { Id = 86, ThaiName = "ชุมพร" },
            new Province { Id = 90, ThaiName = "สงขลา" },
            new Province { Id = 91, ThaiName = "สตูล" },
            new Province { Id = 92, ThaiName = "ตรัง" },
            new Province { Id = 93, ThaiName = "พัทลุง" },
            new Province { Id = 94, ThaiName = "ปัตตานี" },
            new Province { Id = 95, ThaiName = "ยะลา" },
            new Province { Id = 96, ThaiName = "นราธิวาส" }
            };
            modelBuilder.Entity<Province>()
                .HasData(provinces);
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

            modelBuilder.Entity<Product>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Product>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            
 
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
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
            
            modelBuilder.Entity<ProductMoveHistory>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ProductMoveHistory>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

        }


        private void ConfigureCategoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMoveHistory>()
                .Property(e => e.MoveType)
                .HasConversion<int>();
            
            modelBuilder.Entity<Category>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Category>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigProvince(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>(entity => { entity.HasKey(e => e.Id); });

            modelBuilder.Entity<Province>(option =>
            {
                option.Property(e => e.Id).ValueGeneratedNever();
            });
        }

        private void ConfigDistrict(ModelBuilder modelBuilder)
        {
            
            
            
            
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne<Province>(e=>e.Province)
                    .WithMany(x => x.Districts)
                    .HasForeignKey(x=>x.ProvinceId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            
            });
            
        
        }
        
        private void ConfigSubDistrict(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubDistrict>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne<District>(x=>x.District)
                    .WithMany(x => x.SubDistricts)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(x=>x.DistrictId);
            });
        }

        private void ConfigPostalCode(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.HasOne<SubDistrict>(e=>e.SubDistrict)
                    .WithMany(e => e.PostalCodes)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(e => e.SubDistrictId);
            });
        }

        private void ConfigAddress(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<Province>(e=>e.Province)
                    .WithMany(x => x.Addresses)
                    .HasForeignKey(x => x.ProvinceId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne<District>(e=>e.District)
                    .WithMany(x => x.Addresses)
                    .HasForeignKey(x => x.DistrictId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne<SubDistrict>(e=>e.SubDistrict)
                    .WithMany(x => x.Addresses)
                    .HasForeignKey(x => x.SubDistrictId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne<PostalCode>(e=>e.PostalCode)
                    .WithMany(x => x.Addresses)
                    .HasForeignKey(x => x.PostalCodeCodeId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
            });
        }

        private void ConfigCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.HasMany<Address>(e => e.Addresses)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
                
                
                
                
                
            });
        }
        
        private void ConfigParcel(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                
                entity.HasOne<Customer>(e => e.Customer)
                    .WithMany(e => e.Parcels)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<ApplicationUser>(e => e.SaleMan)
                    .WithMany(e => e.SoldItem)
                    .HasForeignKey(e => e.SaleManId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
                
                entity.HasOne<ApplicationUser>(e => e.DeliveryMan)
                    .WithMany(e => e.ShippedPackage)
                    .HasForeignKey(e => e.DeliveryManId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<Parcel>()
                .Property(e => e.ParcelStatus)
                .HasConversion<int>();
            
        }

        

        


    }
}