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
    public Guid CategoryId { get; set; } = Guid.NewGuid();

    public string AdminPK { get; set; } = "1";

    public async Task SeedCategoryAndProductAsync()
    {
        var category = new Category {Id = this.CategoryId, Name = "Chemicals",CreatedById = AdminPK};

        _dbContext.Categories.Add(category);

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(), Name = "Chemical Product 1", CategoryId = CategoryId, Quantity = 100
                ,CreatedById = AdminPK
                ,Code = "001"
            },
            new Product
            {
                Id = Guid.NewGuid(), Name = "Chemical Product 2", CategoryId = CategoryId, Quantity = 100
                ,CreatedById = AdminPK
                ,Code = "002"
            },
            new Product {Id = Guid.NewGuid(), Name = "Chemical Product 3", CategoryId = CategoryId, Quantity = 100
                ,CreatedById = AdminPK
                ,Code = "003"
            }
        };

        _dbContext.Products.AddRange(products);

        await _dbContext.SaveChangesAsync();
    }
}