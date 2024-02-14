using SPCaemucals.Backend.Dto.Product;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Repositories;
using SPCaemucals.Backend.Wrappers;

namespace SPCaemucals.Backend.Services;

public interface IProductService
{
    public Task<PagedResponse<List<ProductResponse>>> GetPagedAsync(PaginationFilter filter, SearchBody body);
    public Task<ProductResponse?> GetAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<PagedResponse<List<ProductResponse>>> GetPagedAsync(PaginationFilter filter, SearchBody body)
    {
        return _productRepository.GetPagedAsync(filter, body);
    }

    public Task<ProductResponse?> GetAsync(int id)
    {
        return _productRepository.GetAsync(id);
    }
}