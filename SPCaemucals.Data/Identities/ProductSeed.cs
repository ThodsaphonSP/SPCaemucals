using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class ProductSeed
{
    private readonly ApplicationDbContext _dbContext;

    public ProductSeed(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public string AdminPK { get; set; } = "1";

    public Category Category { get; set; }

    public async Task SeedCategoryAndProductAsync()
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                  if (!_dbContext.Vendors.Any())
        {
            List<Vendor> vendors = new List<Vendor>()
            {
                new Vendor() {  Name = "Vendor A" },
                new Vendor() {  Name = "Vendor B" },
            };
        
        
            _dbContext.Vendors.AddRange(vendors);

            await _dbContext.SaveChangesAsync();

            this.Vendors = vendors;
        }

        if (!_dbContext.UnitOfMeasurements.Any())
        {
            var unitOfMeasurement = new UnitOfMeasurement {  Name = "Pcs" };
        
        
        
            _dbContext.UnitOfMeasurements.Add(unitOfMeasurement);

            await _dbContext.SaveChangesAsync();

            this.Measure = unitOfMeasurement;
        }




        if (!_dbContext.Categories.Any())
        {
            var category = new Category
            {
                 Name = "Chemicals"
                ,CreatedById = AdminPK
            };

            _dbContext.Categories.Add(category);
            this.Category = category;
        }

        if (!_dbContext.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                     Name = "Chemical Product 1", Category = this.Category, Quantity = 100
                    ,CreatedById = AdminPK
                    ,Code = "001"
                    ,UnitOfMeasurement = Measure
                    ,Vendor = Vendors[0]
                },
                new Product
                {
                     Name = "Chemical Product 2", Category = this.Category, Quantity = 100
                    ,CreatedById = AdminPK
                    ,Code = "002"
                    ,UnitOfMeasurement = Measure
                    ,Vendor = Vendors[0]
                },
                new Product { Name = "Chemical Product 3", Category = this.Category, Quantity = 100
                    ,CreatedById = AdminPK
                    ,Code = "003"
                    ,UnitOfMeasurement = Measure
                    ,Vendor = Vendors[0]
                }
            };

            _dbContext.Products.AddRange(products);
        }
        
        transaction.Commit();

       

        await _dbContext.SaveChangesAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });

      
    }

    public List<Vendor> Vendors { get; set; }

    public UnitOfMeasurement Measure { get; set; }
}