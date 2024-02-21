using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorDeliveryController : ControllerBase
    {
        private readonly ILogger<VendorDeliveryController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public VendorDeliveryController(ILogger<VendorDeliveryController> logger,ApplicationDbContext dbContext,IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(DeliveryVendorDTO))]
        public async Task<IActionResult> GetAllDeliveryVendor(string name = "")
        {
            IQueryable<DeliveryVendor> queryable = _dbContext.DeliveryVendors.AsQueryable();
            
            if (!string.IsNullOrEmpty(name))
            {
                queryable = queryable.Where(v => v.Name.Contains(name));
            }

            var resultQuery = await queryable.ToListAsync();
            List<DeliveryVendorDTO> dto = _mapper.Map<List<DeliveryVendorDTO>>(resultQuery);

            return Ok(dto);
        }
    }
}
