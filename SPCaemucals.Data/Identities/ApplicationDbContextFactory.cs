using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SPCaemucals.Data.Identities;

/// <summary>
/// Factory class for creating instances of ApplicationDbContext.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    /// Creates a new instance of the ApplicationDbContext class.
    /// </summary>
    /// <param name="args">An array of arguments passed to the application.</param>
    /// <returns>A new instance of the ApplicationDbContext class.</returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
 
        // Get connection string
        
        
        // Provide options
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=sw;User Id=sa;Password=A11228803a!3;Encrypt=False;");
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
