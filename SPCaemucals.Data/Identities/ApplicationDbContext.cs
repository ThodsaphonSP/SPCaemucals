using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
        
        public DbSet<Title> Titles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        
        public DbSet<DeliveryVendor> DeliveryVendors { get; set; }
        public DbSet<ProductParcel> ProductParcels { get; set; }
        
        public DbSet<Car> Cars { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<JobService> JobServices { get; set; }

        public DbSet<JobType> JobTypes { get; set; }


        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureApplicationUserEntity(modelBuilder); 

            ConfigureApplicationUserRoleEntity(modelBuilder);



    

            ConfigureProductMoveHistoryEntity(modelBuilder);

            ConfigureProductEntity(modelBuilder);
            ConfigureCategoryEntity(modelBuilder);


            ConfigProvince(modelBuilder);
            ConfigDistrict(modelBuilder);
            ConfigSubDistrict(modelBuilder);
            ConfigPostalCode(modelBuilder);

            ConfigAddress(modelBuilder);
            
            
            ConfigCustomer(modelBuilder);

            ConfigParcel(modelBuilder);
            ConfigTitle(modelBuilder);

            SeedTitle(modelBuilder);
            
            ConfigureUnitOfMeasurement(modelBuilder);
            ConfigVendor(modelBuilder);

            SeedDeliveryVendor(modelBuilder);

            ConfigProductParcel(modelBuilder);

            ConfigComany(modelBuilder);

            ConfigDeliveryVendor(modelBuilder);

            ConfigCar(modelBuilder);

            ConfigJobservice(modelBuilder);
            
            ConfigJob(modelBuilder);

            ConfigJobType(modelBuilder);


        }

        private void ConfigJobType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany<Job>(x => x.Jobs)
                    .WithOne(x => x.JobType)
                    .HasForeignKey(x => x.JobTypeId);
            });
        }

        private void ConfigJobservice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobService>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany<Job>(x => x.Jobs)
                    .WithOne(x => x.JobService)
                    .HasForeignKey(x => x.JobServiceId);

            });
        }
        
        private void ConfigJob(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.Id);

            });
        }

        private void ConfigCar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne<ApplicationUser>(e => e.DeliveryMan)
                    .WithOne()
                    .HasForeignKey<Car>(e => e.DeliveryManId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigComany(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasOne<Address>(e => e.Address)
                    .WithOne()
                    .HasForeignKey<Company>(e => e.AddressId);
            });
        }

        private void ConfigProductParcel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductParcel>(entity =>
            {
                entity.HasKey(e => e.Id);

                
                
                entity.HasOne<Product>(e => e.Product)
                    .WithMany(e => e.ProductParcels)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne<Parcel>(e => e.Parcel)
                    .WithMany(e => e.ProductParcels)
                    .HasForeignKey(e => e.ParcelId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
            });
        }

        private void SeedDeliveryVendor(ModelBuilder modelBuilder)
        {
            List<DeliveryVendor> vendors = new List<DeliveryVendor>()
            {
                new DeliveryVendor()
                {
                    Id = 1,
                    Name = "Flash"
                },
                new DeliveryVendor()
                {
                    Id = 2,
                    Name = "DHL"
                },
                new DeliveryVendor()
                {
                    Id = 3,
                    Name = "ไปรษณีย์ไทย"
                },
                
            };
            modelBuilder.Entity<DeliveryVendor>().HasData(vendors);
        }

       

        private void ConfigureUnitOfMeasurement(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                
                entity.HasMany<Product>(e => e.Product)
                    .WithOne(e => e.UnitOfMeasurement)
                    .IsRequired(false)
                    .HasForeignKey(e => e.UnitOfMeasurementId);
            });
        }

        public void ConfigVendor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);

                

                entity.HasMany<Product>(e => e.Product)
                    .WithOne(e=>e.Vendor)
                    .HasForeignKey(e=>e.VendorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
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

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne<Address>(e => e.Address)
                    .WithOne(e => e.Employee)
                    .HasForeignKey<ApplicationUser>(e => e.AddressId)
                    .IsRequired(false);
                
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
            });
            
            
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

        private void SeedTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>(e =>
            {
                List<Title> titles = new List<Title>(
                new []
                {
                    new Title(){Id = 1,Name = "นาย"},
                    new Title(){Id = 2,Name = "นางสาว"}
                }
                    );
                e.HasData(titles);
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
                .HasOne(p => p.SubstituteProduct)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
  
            
 
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Product>().Property(b => b.Multiplier).HasDefaultValue(1.00m);
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
        
        
        private void ConfigTitle(ModelBuilder modelBuilder)
        {
            
            
            
            
            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => e.Id);
              
                entity.HasMany<Customer>(e=>e.Customers)
                    .WithOne(x => x.Title)
                    .HasForeignKey(x=>x.TitleId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            
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
                
                
                entity.HasOne<Address>(e => e.Addresses)
                .WithOne(e => e.Customer)
                .HasForeignKey<Customer>(e => e.AddressId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Title>(e => e.Title)
                    .WithMany(e => e.Customers)
                    .HasForeignKey(e => e.TitleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
                modelBuilder.Entity<Customer>()
                    .Property(e => e.CustomerType)
                    .HasConversion<int>();
                
                entity.HasIndex(e => e.PhoneNo).IsUnique();

            });
        }
        
        private void ConfigParcel(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                
                
                entity.HasOne<Customer>(e => e.Customer)
                    .WithMany(e => e.Parcels)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<ApplicationUser>(e => e.SaleMan)
                    .WithMany(e => e.SoldItem)
                    .HasForeignKey(e => e.SaleManId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                
                
                entity.HasOne<Car>(e => e.Car)
                    .WithMany(e => e.Parcel)
                    .HasForeignKey(e => e.CarId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DeliveryVendor)
                    .WithMany(e => e.Parcels)
                    .IsRequired()
                    .HasForeignKey(e => e.DeliveryVendorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<Parcel>()
                .Property(e => e.ParcelStatus)
                .HasConversion<int>();
            
        }

        private void ConfigDeliveryVendor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryVendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                
            });
        }

        

        


    }
}