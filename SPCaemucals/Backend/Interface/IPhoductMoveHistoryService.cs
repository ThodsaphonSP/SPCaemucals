using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Interface;

public interface IPhoductMoveHistoryService
{
    Task<List<ProductMoveHistory>> GetHistory();
    Task<ProductMoveHistory?> GetHistory(int id);
}