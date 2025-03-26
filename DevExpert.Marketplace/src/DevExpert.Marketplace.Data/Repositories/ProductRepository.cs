using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Data.Repositories;

public class ProductRepository(MarketplaceContext context)
    : Repository<Product>(context), IProductRepository
{
    public async Task<Product?> GetProductByCategoryIdAsync(Guid categoryId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.CategoryId == categoryId);
    }

    public async Task<Product?> GetProductBySellerIdAsync(Guid sellerId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.SellerId == sellerId);
    }
}