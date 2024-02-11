
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SPCaemucals.Data.Models; // Ensure this is the correct namespace for your models
using SPCaemucals.Data.Identities; // Ensure this is the correct namespace

namespace SPCaemucals.Excel;

public class SeedProvince
{
    private readonly ApplicationDbContext _context;

    public SeedProvince(ApplicationDbContext context)
    {
        _context = context;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context for EPPlus
    }

    public async Task ExecuteSqlFromFileAsync(string filePath)
    {
        if (_context.Provinces.Any())
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        string sqlCommand = await File.ReadAllTextAsync(filePath);

        // Start a new transaction
        using (var transaction = _context.Database.BeginTransaction())
        {
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Provinces ON");

            await _context.Database.ExecuteSqlRawAsync(sqlCommand);

            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Provinces OFF");

            // Commit transaction
            transaction.Commit();
        }
    }
}