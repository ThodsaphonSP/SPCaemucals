using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Services;

namespace SPCaemucals.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllAsync();
        return Ok(response);
    }
}
