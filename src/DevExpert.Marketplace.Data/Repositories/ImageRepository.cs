using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Data.Repositories;

public class ImageRepository(MarketplaceContext context)
    : Repository<Image>(context), IImageRepository
{
    public async Task<List<Image>> GetImagesByProductIdAsync(Guid productId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }
}