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
    public int CategoryId { get; set; } = 1;

    public string AdminPK { get; set; } = "1";

    public async Task SeedCategoryAndProductAsync()
    {

        if (!_dbContext.Vendors.Any())
        {
            List<Vendor> vendors = new List<Vendor>()
            {
                new Vendor() { Id = 1, Name = "Vendor A" },
                new Vendor() { Id = 2, Name = "Vendor B" },
            };
        
        
            _dbContext.Vendors.AddRange(vendors);

            await _dbContext.SaveChangesAsync();
        }

        if (!_dbContext.UnitOfMeasurements.Any())
        {
            var unitOfMeasurement = new UnitOfMeasurement { Id = 1, Name = "Pcs" };
        
        
        
            _dbContext.UnitOfMeasurements.Add(unitOfMeasurement);

            await _dbContext.SaveChangesAsync();
        }




        if (!_dbContext.Categories.Any())
        {
            var category = new Category
            {
                Id = this.CategoryId
                , Name = "Chemicals"
                ,CreatedById = AdminPK
            };

            _dbContext.Categories.Add(category);
        }

        if (!_dbContext.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1, Name = "Chemical Product 1", CategoryId = CategoryId, Quantity = 100
                    ,CreatedById = AdminPK
                    ,Code = "001"
                    ,UnitOfMeasurementId = 1
                    ,VendorId = 1
                },
                new Product
                {
                    Id = 2, Name = "Chemical Product 2", CategoryId = CategoryId, Quantity = 100
                    ,CreatedById = AdminPK
                    ,Code = "002"
                    ,UnitOfMeasurementId = 1
                    ,VendorId = 1
                },
                new Product {Id = 3, Name = "Chemical Product 3", CategoryId = CategoryId, Quantity = 100
                    ,CreatedById = AdminPK
                    ,Code = "003"
                    ,UnitOfMeasurementId = 1
                    ,VendorId = 1
                }
            };

            _dbContext.Products.AddRange(products);
        }

       

        await _dbContext.SaveChangesAsync();
    }
}