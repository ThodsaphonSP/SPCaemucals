using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Interface;
using SPCaemucals.Data.Identities;
using SPCaemucals.Utility;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private new readonly ILogger<CompanyController> _logger;

        private ICompanyRepository _companyRepository;
        private readonly CorrelationIdHelper _correlationIdHelper;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository,CorrelationIdHelper correlationIdHelper) 
        {
            _logger = logger;
            _companyRepository = companyRepository;
            _correlationIdHelper = correlationIdHelper;
        }
        
        [HttpGet()]
        public async Task<IActionResult> GetAllCompany( string name="")
        {
            _logger.LogInformation("correlateId: {@GetCorrelationId} | query company with {@name}",_correlationIdHelper.GetCorrelationId(),name);
            
            List<Company> companies = await _companyRepository.GetAllCompanyAsync( name);

            return Ok(companies);
            
        }
    }
}
