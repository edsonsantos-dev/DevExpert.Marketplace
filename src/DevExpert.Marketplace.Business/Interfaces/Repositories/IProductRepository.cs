using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
    Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId);
}