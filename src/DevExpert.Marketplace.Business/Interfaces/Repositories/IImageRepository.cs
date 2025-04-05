using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Interfaces.Repositories;

public interface IImageRepository : IRepository<Image>
{
    Task<List<Image>> GetImagesByProductIdAsync(Guid productId);
}