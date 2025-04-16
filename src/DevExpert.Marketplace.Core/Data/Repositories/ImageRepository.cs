using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Domain.Images;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Core.Data.Repositories;

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

    public async Task DeleteRangeByProductIdAsync(Guid productId)
    {
        await DbSet.Where(x => x.ProductId == productId).ExecuteDeleteAsync();
    }
}