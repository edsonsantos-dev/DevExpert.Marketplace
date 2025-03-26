using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Services;

public class ProductService(IProductRepository repository, INotifier notifier)
    : Service<Product>(repository, notifier), IProductService
{
    public async Task<Product?> GetProductByCategoryIdAsync(Guid categoryId)
    {
        return await repository.GetProductByCategoryIdAsync(categoryId);
    }

    public async Task<Product?> GetProductBySellerIdAsync(Guid sellerId)
    {
        return await repository.GetProductBySellerIdAsync(sellerId);
    }
}