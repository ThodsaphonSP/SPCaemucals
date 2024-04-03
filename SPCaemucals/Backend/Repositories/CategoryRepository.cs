using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Repositories;


public interface ICategoryRepository
{
    public Task<List<Category>> GetAllAsync();
    Task<PagedList<Category>> GetCategory(int pageNumber, int pageSize, string name);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Category>> GetAllAsync()
    {
        return _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<PagedList<Category>> GetCategory(int pageNumber, int pageSize, string name)
    {
        var query = _context.Categories.AsQueryable();

        if (string.IsNullOrEmpty(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        PagedList<Category> page = await PagedList<Category>.CreateAsync(query, pageNumber, pageSize);

        return page;
    }
}