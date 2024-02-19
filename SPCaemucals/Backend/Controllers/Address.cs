using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Controllers
{
  
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Address : ControllerBase
    {
        private readonly ILogger<Address> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public Address(ILogger<Address> logger,ApplicationDbContext dbContext,IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }



        /// <summary>
        /// Fetches postal codes based on the provided sub district ID.
        /// </summary>
        /// <param name="subDistrictId">An Id of the sub district. The default value is 0.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /PostalName?subDistrictId=1
        ///     
        /// Sample response:
        ///
        ///     [
        ///         {
        ///             "id":1,
        ///             "code": "1000",
        ///             "subDistrictId": 1
        ///         },
        ///         {
        ///             "id":2,
        ///             "code": "2000",
        ///             "subDistrictId": 1
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>List of Postal Codes</returns>
        /// <response code="200">Returns the list of postal codes</response>
        /// <response code="400">If the provided sub district id is invalid</response>

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PostalDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Postal")]
        public async Task<IActionResult> GetPostalCode(int subDistrictId = 0)
        {
            _logger.LogInformation("Start fetching Postal Code data for SubDistrict ID: {SubDistrictId}",
                subDistrictId);

            if (subDistrictId < 0)
            {
                _logger.LogError("Invalid sub-district ID: {SubDistrictId}", subDistrictId);
                return BadRequest("Invalid sub-district ID.");
            }

            IQueryable<PostalCode> postalCodeQuery = _dbContext.PostalCodes;

            if (subDistrictId != 0)
            {
                postalCodeQuery = postalCodeQuery.Where(x => x.SubDistrictId == subDistrictId);
            }

            postalCodeQuery = postalCodeQuery.OrderBy(x => x.Code);

            postalCodeQuery = postalCodeQuery.Take(1000);

            _logger.LogInformation("Executing the query to get postal codes for SubDistrict ID: {SubDistrictId}",
                subDistrictId);

            List<PostalCode> sortedPostalCodes;

            try
            {
                sortedPostalCodes = await postalCodeQuery.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while fetching Postal Code data for SubDistrict ID: {SubDistrictId}",
                    subDistrictId);

                throw; // or return some error response
            }

            _logger.LogInformation("Postal Code data fetched successfully for SubDistrict ID: {SubDistrictId}",
                subDistrictId);

            var result = sortedPostalCodes.Select(x => new PostalDTO
                { Code = x.Code, SubDistrictId = x.SubDistrictId, Id = x.Id });

        return Ok(result);
        }

        [HttpGet("Province")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<ProvinceDTO>))]
        public async Task<List<ProvinceDTO>> GetProvince()
        {
            IQueryable<Province> queryable = _dbContext.Provinces.AsQueryable();
            

            queryable = queryable.OrderBy(x => x.ThaiName);
            var result = await queryable.ToListAsync();


            var newResult = _mapper.Map<List<Province>, List<ProvinceDTO>>(result);
            return newResult;

        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DistrictDTO>))]
        [HttpGet("District")]
        public async Task<List<DistrictDTO>> GetDistrict([Required]int provinceId )
        {
            IQueryable<District> queryable = _dbContext.Districts;
            if (provinceId != 0)
            {
                queryable = queryable.Where(x => x.ProvinceId == provinceId).Include(x=>x.SubDistricts).ThenInclude(x=>x.PostalCodes);
            }
            queryable = queryable.OrderBy(x => x.ThaiName);
            var result = await queryable.ToListAsync();
            
            var newResult = _mapper.Map<List<District>, List<DistrictDTO>>(result);
            return newResult;
        }
        
        
        [HttpGet("SubDistrict")]
        public async Task<List<SubDistrict>> GetSubDistrict(int district = 0)
        {
            IQueryable<SubDistrict> queryable = _dbContext.SubDistricts;
            if (district != 0)
            {
                queryable = queryable.Where(x => x.DistrictId == district);
            }
            queryable = queryable.OrderBy(x => x.ThaiName);

            queryable = queryable.Take(1000);
            var result = await queryable.ToListAsync();
            return result;
        }
        
    }
}
