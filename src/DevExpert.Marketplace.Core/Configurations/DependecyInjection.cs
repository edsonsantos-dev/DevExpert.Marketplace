using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Data.Repositories;
using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Domain.Products;
using DevExpert.Marketplace.Core.Domain.Sellers;
using DevExpert.Marketplace.Core.Helpers;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DevExpert.Marketplace.Core.Configurations;

public static class DependecyInjection
{
    public static void RegisterDependency(this WebApplicationBuilder builder)
    {
        var service = builder.Services;
        
        RegisterApplicationDependencies(service);
        RegisterRepositories(service);
        RegisterServices(service);
    }

    private static void RegisterApplicationDependencies(IServiceCollection service)
    {
        service.AddScoped<IUserContext, UserContext>();
        service.AddScoped<MarketplaceContext>();
        service.AddScoped<INotifier, Notifier>();
    }

    private static void RegisterRepositories(IServiceCollection service)
    {
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IImageRepository, ImageRepository>();
        service.AddScoped<ISellerRepository, SellerRepository>();
    }

    private static void RegisterServices(IServiceCollection service)
    {
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<ISellerService, SellerService>();
    }
}