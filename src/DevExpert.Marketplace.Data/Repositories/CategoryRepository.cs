using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Data.Repositories;

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