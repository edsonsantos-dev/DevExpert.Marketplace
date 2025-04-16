using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ProductHasImageAsync(Guid id);
    Task<List<Product>> GetProductsByCategoriesIdAsync(List<Guid> categoreisId);
    Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId);
}