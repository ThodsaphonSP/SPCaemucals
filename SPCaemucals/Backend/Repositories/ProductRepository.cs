using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto.Product;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Wrappers;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Repositories;


public interface IProductRepository
{
    public Task<PagedResponse<List<ProductResponse>>> GetPagedAsync(PaginationFilter filter, SearchBody body);
    public Task<ProductResponse?> GetAsync(Guid id);
}

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<List<ProductResponse>>> GetPagedAsync(PaginationFilter filter, SearchBody body)
    {
        var productQuery = _context.Products.AsQueryable();

        if (body.CategoryId.HasValue)
        {
            productQuery = productQuery.Where(e => e.CategoryId == body.CategoryId);
        }

        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var products = await productQuery
            .Skip(validFilter.Skip())
            .Take(validFilter.Take())
            .Include(e => e.Category)
            .AsSplitQuery()
            .AsNoTracking()
            .Select(e => new ProductResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                CategoryId = e.CategoryId,
                CategoryName = e.Category.Name,
                Price = e.Price,
                Quantity = e.Quantity,
            })
            .ToListAsync();

        var total = await _context.Products.CountAsync();

        return new PagedResponse<List<ProductResponse>>(products, validFilter.PageNumber, validFilter.PageSize, total);
    }

    public Task<ProductResponse?> GetAsync(Guid id)
    {
        return _context.Products
            .Include(e => e.Category)
            .AsNoTracking()
            .Select(e => new ProductResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                CategoryId = e.CategoryId,
                CategoryName = e.Category.Name,
                Price = e.Price,
                Quantity = e.Quantity,
            })
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}