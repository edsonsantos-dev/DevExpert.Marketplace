using DevExpert.Marketplace.Application.Helpers;
using DevExpert.Marketplace.Business.Interfaces;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Business.Notifications;
using DevExpert.Marketplace.Business.Services;
using DevExpert.Marketplace.Data.Context;
using DevExpert.Marketplace.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DevExpert.Marketplace.IoC;

public static class BootStrapper
{
    public static void RegisterIoC(this WebApplicationBuilder builder)
    {
        var service = builder.Services;

        service.AddScoped<IUserContext, UserContext>();
        service.AddScoped<MarketplaceContext>();
        service.AddScoped<INotifier, Notifier>();

        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IImageRepository, ImageRepository>();
        service.AddScoped<ISellerRepository, SellerRepository>();

        service.AddScoped<IService<Category>, CategoryService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<IService<Seller>, SellerService>();
    }
}