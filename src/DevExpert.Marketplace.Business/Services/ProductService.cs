using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Services;

public class ProductService(IProductRepository repository, INotifier notifier)
    : Service<Product>(repository, notifier), IProductService
{
    public async override Task<Product> AddAsync(Product entity)
    {
        entity.Validation(notifier);
        
        if (notifier.HaveNotification())
            return entity;
        
        if (entity.Images.Any(image => !image.SaveImage(notifier, entity.Id)))
            return entity;

        entity = await repository.AddAsync(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    public async Task<Product?> GetProductByCategoryIdAsync(Guid categoryId)
    {
        return await repository.GetProductByCategoryIdAsync(categoryId);
    }

    public async Task<Product?> GetProductBySellerIdAsync(Guid sellerId)
    {
        return await repository.GetProductBySellerIdAsync(sellerId);
    }
}