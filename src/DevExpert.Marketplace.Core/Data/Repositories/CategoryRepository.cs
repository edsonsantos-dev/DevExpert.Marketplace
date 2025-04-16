using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Core.Data.Repositories;

public class CategoryRepository(MarketplaceContext context)
    : Repository<Category>(context), ICategoryRepository
{
    public async Task<Category?> GetCategoryWithProductsAsync(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}