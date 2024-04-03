using System.Data.Common;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Services;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Utility;

namespace SPCaemucals.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ProductController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger,IProductService productService,ApplicationDbContext dbContext,IMapper mapper)
    {
        _logger = logger;
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


    /// <summary>
    /// Creates a new product in the system.
    /// </summary>
    /// <param name="body">A DTO object representing the product to be created.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is an IActionResult
    /// that represents a result of the web request.</returns>
    /// <response code="200">Product created successfully.</response>
    /// <response code="500">If there is a database update exception.</response>
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO body)
{
    return await _dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
               return await CreateProductInternal(body, transaction);
            }
            catch (DbUpdateException ex) 
            {
               return HandleException(ex, transaction);
            }
        }
    });
}

    private async Task<IActionResult> CreateProductInternal(ProductDTO body,
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction)
    {
        _logger.LogInformation("Product creation initiated.");
        var product = _mapper.Map<Product>(body);
        if (IsProductNotExist(product))
        {
            
            
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            var productDTO = _mapper.Map<ProductDTO>(product);
            transaction.Commit();
            _logger.LogInformation("Product creation successful. Product Id: {ProductId}", productDTO.Id);
            return CreatedAtAction(nameof(CreateProduct), new { id = productDTO.Id }, productDTO);
        }
        else
        {
            _logger.LogInformation("Product already exists. Cannot create duplicate product.");
            return BadRequest("Product already exists.");
        }
    }


    private bool IsProductNotExist(Product product)
{
    return !_dbContext.Products.Any(x => x.Name == product.Name);
}

    private IActionResult HandleException(DbUpdateException ex, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction)
{
    const string LogMessage = "Product creation failed. Exception: {ExceptionMessage}, Inner Exception: {InnerExceptionMessage}";
    const string ErrorMessage = "Internal server error: {ex.Message}, Inner Exception: {innerExceptionMessage}";
    const int InternalServerErrorStatusCode = 500;

    transaction.Rollback();
    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
    _logger.LogError(ex, LogMessage, ex.Message, innerExceptionMessage);
    return StatusCode(InternalServerErrorStatusCode, string.Format(ErrorMessage, ex.Message, innerExceptionMessage));
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