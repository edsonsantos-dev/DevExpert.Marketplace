using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetProductByCategoryIdAsync(Guid categoryId);
    Task<Product?> GetProductBySellerIdAsync(Guid sellerId);
}