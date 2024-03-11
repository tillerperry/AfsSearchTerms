using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Afs.SearchTerms.Web.DataContext;

public class SampleDataContext : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configurationRoot.GetConnectionString("DbConnection");
        
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}