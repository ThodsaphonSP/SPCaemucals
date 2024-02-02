using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Dto.Product;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Backend.Services;

namespace SPCaemucals.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost(nameof(GetPaged))]
    public async Task<IActionResult> GetPaged([FromQuery] PaginationFilter filter, [FromBody] SearchBody body)
    {
        var response = await _productService.GetPagedAsync(filter, body);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid id)
    {
        var response = await _productService.GetAsync(id);
        if(response is null)
        {
            return NotFound();
        }

        return Ok(response);
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