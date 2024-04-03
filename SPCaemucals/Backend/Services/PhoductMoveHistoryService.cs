using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Interface;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Services;

class PhoductMoveHistoryService : IPhoductMoveHistoryService
{
    private readonly ApplicationDbContext _dbContext;

    public PhoductMoveHistoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<ProductMoveHistory>> GetHistory()
    {
        var query     =  _dbContext.ProductMoveHistories.Select(x=>x);

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<ProductMoveHistory?> GetHistory(int id)
    {
        var result = await _dbContext.ProductMoveHistories.SingleOrDefaultAsync(x => x.Id == id);

        return result;
    }
}