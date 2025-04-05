using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Data.Repositories;

public class ProductRepository(MarketplaceContext context)
    : Repository<Product>(context), IProductRepository
{
    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x=> x.Images)
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .Include(x => x.Images)
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .Include(x => x.Images)
            .Where(x => x.SellerId == sellerId)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .Include(x => x.Images)
            .ToListAsync();
    }
}