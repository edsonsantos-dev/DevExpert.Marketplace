using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Services;

public class ProductService(
    IProductRepository repository,
    IImageRepository imageRepository,
    INotifier notifier)
    : Service<Product>(repository, notifier), IProductService
{
    public async Task<bool> ProductHasImageAsync(Guid id)
    {
        return await repository.ProductHasImageAsync(id);
    }

    public override async Task<Product> UpdateAsync(Product entity)
    {
        var hasImage = false;
        if (entity.Images.Count == 0)
            hasImage = await ProductHasImageAsync(entity.Id);

        if (!hasImage) 
            return await base.UpdateAsync(entity);
        
        var images = await imageRepository.GetImagesByProductIdAsync(entity.Id);
        entity.Images.AddRange(images);

        return await base.UpdateAsync(entity);
    }

    public async Task<List<Product>> GetProductsByCategoriesIdAsync(List<Guid> categoreisId)
    {
        return await repository.GetProductsByCategoriesIdAsync(categoreisId);
    }

    public async Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId)
    {
        return await repository.GetProductsBySellerIdAsync(sellerId);
    }
}