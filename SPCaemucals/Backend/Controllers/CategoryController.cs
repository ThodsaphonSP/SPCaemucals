using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Repositories;
using SPCaemucals.Backend.Services;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoryController> _logger;


    public CategoryController(ICategoryRepository repository,ILogger<CategoryController> logger)
    {
        _repository = repository;
        _logger = logger;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Category>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpGet]
    public async Task<IActionResult> GetCategories(int pageNumber = 1, int pageSize = 10, string name="")
    {
        // Validate inputs
        if (pageSize <= 0 || pageNumber <= 0)
        {
            _logger.LogWarning("Invalid pagination parameter");
            return BadRequest("Invalid pagination parameter");
        }

        if (pageSize > 100)
        {
            _logger.LogWarning("Page size cannot be greater than 100");
            return BadRequest("Page size cannot be greater than 100");
        }

        

        try
        {
            // Call repository to get data
            PagedList<Category> response = await _repository.GetCategory(pageNumber, pageSize, name);
        
            // Log and return response
            _logger.LogInformation("Retrieved {Count} categories for page number {PageNumber} with page size {PageSize} and name {Name}", response.Count, pageNumber, pageSize, name);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log error and return error response
            _logger.LogError(ex, "Error retrieving categories {@ex}",ex);
            return StatusCode(500, "An error occurred while fetching the categories");
        }
    }
}
