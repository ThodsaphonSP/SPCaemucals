using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SPCaemucals.Data.Identities;

/// <summary>
/// Factory class for creating instances of ApplicationDbContext.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=sw;User Id=sa;Password=A11228803a!3;Encrypt=False;");


        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
