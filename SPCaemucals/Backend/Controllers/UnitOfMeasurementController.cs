

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UnitOfMeasurementController : ControllerBase
{
    private readonly ILogger<UnitOfMeasurementController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UnitOfMeasurementController(ILogger<UnitOfMeasurementController> logger,ApplicationDbContext dbContext,IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UnitOfMeasurementDTO))]
    public async Task<IActionResult> GetAllUnit()
    {
        var units = await _dbContext.UnitOfMeasurements.ToListAsync();
        var unitDTOs = _mapper.Map<List<UnitOfMeasurementDTO>>(units);
        return Ok(unitDTOs);
    }
    
}