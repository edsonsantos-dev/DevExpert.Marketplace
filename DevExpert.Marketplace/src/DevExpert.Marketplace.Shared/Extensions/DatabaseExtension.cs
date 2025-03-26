using DevExpert.Marketplace.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevExpert.Marketplace.Shared.Extensions;

public static class DatabaseExtension
{
    public static void RegisterDatabaseServices(this WebApplicationBuilder webApplicationBuilder)
    {
        if (webApplicationBuilder.Environment.IsDevelopment())
        {
            webApplicationBuilder.Services.AddDbContext<MarketplaceContext>(options =>
                options.UseSqlite(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnectionLite")));
        }
        else
        {
            webApplicationBuilder.Services.AddDbContext<MarketplaceContext>(options =>
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));
        }
    }
    
    public static async Task EnsureSeedData(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var env = scopedProvider.GetRequiredService<IHostEnvironment>();
        var marketplaceContext = scopedProvider.GetRequiredService<MarketplaceContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await marketplaceContext.Database.MigrateAsync();
        }
    }
}