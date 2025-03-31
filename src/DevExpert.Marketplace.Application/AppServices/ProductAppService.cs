using DevExpert.Marketplace.Application.Helpers;
using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.AppServices;

public class ProductAppService(
    IProductService service,
    INotifier notifier)
    : AppService<Product, ProductInputViewModel, ProductOutputViewModel>(service), IProductAppService
{
    public override async Task<ProductOutputViewModel> AddAsync(ProductInputViewModel inputViewModel)
    {
        var product = inputViewModel.ToModel();
        await ImageHelper.CreateImageAsync(product, inputViewModel.Images, notifier);
        product = await service.AddAsync(product);
        return new ProductOutputViewModel().FromModel(product);
    }

    public async Task<List<ProductOutputViewModel>> GetProductsByCategoryIdAsync(Guid categoryId)
    {
        var products = await service.GetProductsByCategoryIdAsync(categoryId);
        return products.Select(p => new ProductOutputViewModel().FromModel(p)).ToList();
    }

    public async Task<List<ProductOutputViewModel>> GetProductsBySellerIdAsync(Guid sellerId)
    {
        var products = await service.GetProductsBySellerIdAsync(sellerId);
        return products.Select(p => new ProductOutputViewModel().FromModel(p)).ToList();
    }

    public override async Task DeleteAsync(Guid id)
    {
        await service.DeleteAsync(id);
        ImageHelper.DeleteImage(id);
    }
}