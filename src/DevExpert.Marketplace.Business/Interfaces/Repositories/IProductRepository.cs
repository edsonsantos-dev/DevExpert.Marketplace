using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ProductHasImageAsync(Guid id);
    Task<List<Product>> GetProductsByCategoriesIdAsync(List<Guid> categoreisId);
    Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId);
}