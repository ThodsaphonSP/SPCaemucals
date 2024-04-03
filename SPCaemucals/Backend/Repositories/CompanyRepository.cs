using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Interface;
using SPCaemucals.Data.Identities;
using SPCaemucals.Utility;

namespace SPCaemucals.Backend.Repositories;

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