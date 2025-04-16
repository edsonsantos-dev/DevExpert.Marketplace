using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Images;

public interface IImageRepository : IRepository<Image>
{
    Task<List<Image>> GetImagesByProductIdAsync(Guid productId);
    Task DeleteRangeByProductIdAsync(Guid productId);
}