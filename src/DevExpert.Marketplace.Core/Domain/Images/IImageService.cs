using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Core.Domain.Images;

public interface IImageService
{
    Task DeleteAsync(Guid id);
    Task ReorderProductImagesDisplayPositionAsync(List<Image> images);
}