using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Repositories;


public interface ICategoryRepository
{
    public Task<List<Category>> GetAllAsync();
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
}