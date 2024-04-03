using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Repositories;
using SPCaemucals.Backend.Services;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoryController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;


    public CategoryController(ILogger<CategoryController> logger,ApplicationDbContext dbContext,IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get paged categories with name filter option.
    /// </summary>
    /// <remarks>
    /// This API will get the paged list of categories based on 
    /// pageNumber, pageSize and name filter.
    /// </remarks>
    /// <param name="pageNumber">Page number for the result set starting from 1 (default is 1)</param>
    /// <param name="pageSize">Size of the page starting from 1 upto 100 (default is 10)</param>
    /// <param name="name">Name filter for the categories (default is empty)</param>
    /// <response code="200">Returns paged list of categories</response>
    /// <response code="400">If validation of input parameters fails</response>
    /// <response code="500">If there is an server error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
    public async Task<IActionResult> GetCategories(string name="")
    {
        var query = _dbContext.Categories.AsQueryable();
        
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        query = query.OrderBy(x => x.Name);

        var result = await query.ToListAsync();
        
        var newResult = _mapper.Map<List<CategoryDTO>>(result);

        return Ok(result);

    }
}
