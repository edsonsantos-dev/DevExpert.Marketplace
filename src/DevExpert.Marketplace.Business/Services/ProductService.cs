using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Services;

public class ProductService(
    IProductRepository repository,
    INotifier notifier)
    : Service<Product>(repository, notifier), IProductService
{
    public async Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
    {
        return await repository.GetProductsByCategoryIdAsync(categoryId);
    }

    public async Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId)
    {
        return await repository.GetProductsBySellerIdAsync(sellerId);
    }
}