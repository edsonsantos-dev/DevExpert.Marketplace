using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Core.Domain.Images;

public interface IImageService
{
    Task ReorderProductImagesDisplayPositionAsync(List<Image> images);
}