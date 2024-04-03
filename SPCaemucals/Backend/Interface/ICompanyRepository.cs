using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Interface;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllCompanyAsync(string name);
}