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

    public async Task ExecuteSqlFromFileAsync(string filePath)
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
}