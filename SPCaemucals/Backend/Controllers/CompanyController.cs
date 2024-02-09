using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;
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

    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompanyAsync(string name);
    }

    class CompanyRepository : ICompanyRepository
    {
        private readonly ILogger<CompanyRepository> _logger;
        private readonly CorrelationIdHelper _coHelper;
        private readonly ApplicationDbContext _dbContext;

        public CompanyRepository(ILogger<CompanyRepository> logger,CorrelationIdHelper coHelper,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _coHelper = coHelper;
            _dbContext = dbContext;
        }
        public Task<List<Company>> GetAllCompanyAsync(string name)
        {
            
            var correlationId = _coHelper.GetCorrelationId();
            _logger.LogInformation("Correlation ID: {@correlationId}",_coHelper.GetCorrelationId());
            
            var query = _dbContext.Company.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.CompanyName.Contains(name));
            }

            query = query.OrderBy(x => x.CompanyName);

            var result = query.ToListAsync();

            return result;
        }
    }
}
