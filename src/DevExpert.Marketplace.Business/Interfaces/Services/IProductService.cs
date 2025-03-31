using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Interfaces.Services;

public interface IProductService : IService<Product>
{
    Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
    Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId);
}