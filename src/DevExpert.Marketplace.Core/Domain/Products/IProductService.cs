namespace DevExpert.Marketplace.Core.Domain.Products;

public interface IProductService : IDisposable
{
    Task<ProductOutputViewModel> AddAsync(ProductInputViewModel inputViewModel);
    Task<ProductOutputViewModel> GetAsync(Guid id);
    Task<bool> ProductHasImageAsync(Guid id);
    Task<List<ProductOutputViewModel>> GetProductsByCategoriesIdAsync(List<Guid> categoriesId);
    Task<List<ProductOutputViewModel>> GetProductsBySellerIdAsync();
    Task<ProductOutputViewModel> UpdateAsync(ProductInputViewModel inputViewModel);
    Task DeleteAsync(Guid id);
}