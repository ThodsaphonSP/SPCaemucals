using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Dto.Model;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger,ApplicationDbContext dbContext
        ,IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(ReceiverDetails))]
        public async Task<IActionResult> GetCustomers(string phone)
        {
            ReceiverDetails receiverDetails = new ReceiverDetails();


            var customers = await _dbContext.Customers.Where(x => x.PhoneNo == phone)
                .Include(x => x.Addresses)
                .ThenInclude(a => a.Province)
                .Include(x => x.Addresses)
                .ThenInclude(a => a.District)
                .Include(x=>x.Addresses)
                .ThenInclude(x=>x.SubDistrict)
                .Include(x=>x.Addresses)
                .ThenInclude(x=>x.PostalCode)
                .FirstOrDefaultAsync();
            if (customers!= null)
            {
                receiverDetails.Firstname = customers.FirstName;
                receiverDetails.Lastname = customers.LastName;
                receiverDetails.PhoneNo = customers.PhoneNo;
                receiverDetails.AddressText = customers.Addresses.AddressDetail;
                receiverDetails.Province = _mapper.Map<ProvinceDTO>(customers.Addresses.Province);
                receiverDetails.District = _mapper.Map<DistrictDTO>(customers.Addresses.District);
                receiverDetails.SubDistrict = _mapper.Map<SubDistrictDTO>(customers.Addresses.SubDistrict);
                receiverDetails.PostalCode = _mapper.Map<PostalDTO>(customers.Addresses.PostalCode);
            }
            else
            {
                return NotFound();
            }
            
            
            
            return Ok(receiverDetails);
        }
    }
}
