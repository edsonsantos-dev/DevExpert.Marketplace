using DevExpert.Marketplace.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DevExpert.Marketplace.Data.Context;

public class MarketplaceContextFactory : IDesignTimeDbContextFactory<MarketplaceContext>
{
    public MarketplaceContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MarketplaceContext>();

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        if (environment == "Development")
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnectionLite"));
        }
        else
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        
        var userContext = new DummyUserContext();

        return new MarketplaceContext(optionsBuilder.Options, userContext);
    }
}

public class DummyUserContext : IUserContext
{
    public Guid GetUserId() => Guid.Empty;
    public bool IsAuthenticated() => false;
}
