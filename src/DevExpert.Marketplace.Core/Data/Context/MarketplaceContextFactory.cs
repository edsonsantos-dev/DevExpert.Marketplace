using DevExpert.Marketplace.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DevExpert.Marketplace.Core.Data.Context;

public class MarketplaceContextFactory : IDesignTimeDbContextFactory<MarketplaceContext>
{
    public MarketplaceContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            
        var basePath = Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();
            
        var connectionString = configuration.GetConnectionString(nameof(MarketplaceContext));
            
        var optionsBuilder = new DbContextOptionsBuilder<MarketplaceContext>();
        if (environment == "Development")
            optionsBuilder.UseSqlite(connectionString);
        else
            optionsBuilder.UseSqlServer(connectionString);
            
        var userContext = new DesignTimeUserContext();
            
        return new MarketplaceContext(optionsBuilder.Options, userContext);
    }
}

public class DesignTimeUserContext : IUserContext
{
    public Guid GetUserId() => Guid.Empty;
    public bool IsAuthenticated() => false;
}