using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Domain.User;
using DevExpert.Marketplace.Core.Notifications;

namespace DevExpert.Marketplace.Core.Domain.Products;

public class ProductService(
    IImageService imageService,
    IProductRepository repository,
    IImageRepository imageRepository,
    IUserContext userContext,
    INotifier notifier) : IProductService
{
    public async Task<ProductOutputViewModel> AddAsync(ProductInputViewModel inputViewModel)
    {
        var product = inputViewModel.ToModel();
        product.SellerId = userContext.GetUserId();
        await ImageService.CreateImageAsync(product, inputViewModel.Images, notifier);
        product.Validation(notifier);

        if (notifier.HaveNotification())
            return ProductOutputViewModel.FromModel(product);

        product = await repository.AddAsync(product);
        await repository.SaveChangesAsync();

        return ProductOutputViewModel.FromModel(product);
    }

    public async Task<ProductOutputViewModel> GetAsync(Guid id)
    {
        var product = await repository.GetByIdAsync(id);
        return ProductOutputViewModel.FromModel(product);
    }

    public async Task<bool> ProductHasImageAsync(Guid id)
    {
        return await repository.ProductHasImageAsync(id);
    }

    public async Task<List<ProductOutputViewModel>> GetAllAsync()
    {
        var products = await repository.GetAllAsync();
        return products.Select(ProductOutputViewModel.FromModel).ToList();
    }

    public async Task<List<ProductOutputViewModel>> GetProductsByCategoriesIdAsync(List<Guid> categoriesId)
    {
        var products = await repository.GetProductsByCategoriesIdAsync(categoriesId);
        return products.Select(ProductOutputViewModel.FromModel).ToList();
    }

    public async Task<List<ProductOutputViewModel>> GetProductsBySellerIdAsync()
    {
        var products = await repository.GetProductsBySellerIdAsync(userContext.GetUserId());
        return products.Select(ProductOutputViewModel.FromModel).ToList();
    }

    public async Task<ProductOutputViewModel> UpdateAsync(ProductInputViewModel inputViewModel)
    {
        var product = inputViewModel.ToModel();

        if (product.SellerId != userContext.GetUserId())
        {
            notifier.AddNotification(new Notification("A atualização do produto está restrita ao vendedor de origem."));
            return ProductOutputViewModel.FromModel(product);
        }

        if (inputViewModel.Images is { Count: > 0 })
            await ImageService.CreateImageAsync(product, inputViewModel.Images, notifier);

        var images = await imageRepository.GetImagesByProductIdAsync(product.Id);
        product.Images.AddRange(images);

        product.Validation(notifier);

        if (notifier.HaveNotification())
            return ProductOutputViewModel.FromModel(product);

        product = await repository.UpdateAsync(product);
        await repository.SaveChangesAsync();

        await imageService.ReorderProductImagesDisplayPositionAsync(product.Images);

        return ProductOutputViewModel.FromModel(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null)
        {
            notifier.AddNotification(new Notification("Produto não encontrado."));
            return;
        }

        if (product.SellerId != userContext.GetUserId())
        {
            notifier.AddNotification(new Notification("A exclusão do produto está restrita ao vendedor de origem."));
            return;
        }

        await repository.DeleteAsync(product);
        await repository.SaveChangesAsync();
        await imageRepository.DeleteRangeByProductIdAsync(id);

        ImageService.DeleteImage(id);
    }

    public void Dispose()
    {
        repository.Dispose();
    }
}