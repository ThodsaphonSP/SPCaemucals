using SPCaemucals.Backend.Repositories;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Services;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<List<Category>> GetAllAsync()
    {
        return _categoryRepository.GetAllAsync();
    }
}
