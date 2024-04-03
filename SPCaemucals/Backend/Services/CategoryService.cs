using SPCaemucals.Backend.Repositories;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Services;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync(int pageNumber,int pageSize,string name);
}


