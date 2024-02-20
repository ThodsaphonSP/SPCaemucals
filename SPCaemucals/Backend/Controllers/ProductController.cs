using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Dto.Product;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Services;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService,ApplicationDbContext dbContext,IMapper mapper)
    {
        _productService = productService;
        _dbContext = dbContext;
        _mapper = mapper;
    }

   

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ProductDTO))]
    public async Task<IActionResult> Get([FromQuery] int id)
    {

        var query = _dbContext.Products.AsQueryable();
        if (id!= 0)
        {
            query = query.Where(p => p.Id == id);
        }

        var products = await query.ToListAsync();

        var result = _mapper.Map<List<ProductDTO>>(products);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProduct body)
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct()
    {
        return Ok();
    }
}