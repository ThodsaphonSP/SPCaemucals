using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Identities;
// Ensure this is the correct namespace for your models

// Ensure this is the correct namespace



namespace SPCaemucals.Script;

public class Seed
{
    private readonly ApplicationDbContext _context;

    public Seed(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InsertProvinceAsync(string filePath)
    {
        if (_context.Districts.Any())
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        string sqlCommand = await File.ReadAllTextAsync(filePath);

    
        await _context.Database.ExecuteSqlRawAsync(sqlCommand);

    }

    public async Task InsertDistrictAsync(string filePath)
    {
        if (_context.Districts.Any())
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        string sqlCommand = await File.ReadAllTextAsync(filePath);

    
        await _context.Database.ExecuteSqlRawAsync(sqlCommand);
    }
    
    
    public async Task InsertSubDistrictAsync(string filePath)
    {
        if (_context.SubDistricts.Any())
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        string sqlCommand = await File.ReadAllTextAsync(filePath);

    
        await _context.Database.ExecuteSqlRawAsync(sqlCommand);
    }
    
    public async Task InsertPostalAsync(string filePath)
    {
        if (_context.PostalCodes.Any())
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        string sqlCommand = await File.ReadAllTextAsync(filePath);

    
        await _context.Database.ExecuteSqlRawAsync(sqlCommand);
    }
}