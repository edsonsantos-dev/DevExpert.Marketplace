using DevExpert.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.App.Extensions;

public static class DatabaseExtension
{
    public static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<MarketplaceContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString(nameof(MarketplaceContext))));
            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString(nameof(IdentityContext))));
        }
        else
        {
            builder.Services.AddDbContext<MarketplaceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MarketplaceContext))));
            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(IdentityContext))));
        }
    }
    
    public static async Task EnsureSeedData(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var env = scopedProvider.GetRequiredService<IHostEnvironment>();
        var marketplaceContext = scopedProvider.GetRequiredService<MarketplaceContext>();
        var identityContext = scopedProvider.GetRequiredService<IdentityContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await identityContext.Database.MigrateAsync();
            await marketplaceContext.Database.MigrateAsync();
        }
    }
}