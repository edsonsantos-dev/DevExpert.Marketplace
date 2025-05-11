using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Core.Data.Repositories;

public class ProductRepository(MarketplaceContext context)
    : Repository<Product>(context), IProductRepository
{
    public override async Task<Product> UpdateAsync(Product entity)
    {
        entity = await base.UpdateAsync(entity);
        
        foreach (var image in entity.Images.Where(x => x.AddedOn == DateTime.MinValue))
            context.Entry(image).State = EntityState.Added;
        
        return entity;
    }

    public async Task<bool> ProductHasImageAsync(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Images)
            .AnyAsync(x => x.Id == id && x.Images.Any());
    }
    
    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x=> x.Images)
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Product>> GetProductsByCategoriesIdAsync(List<Guid> categoreisId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .Include(x => x.Images)
            .Where(x => categoreisId.Contains(x.CategoryId))
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