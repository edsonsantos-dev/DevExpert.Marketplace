using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Core.Domain.Images;

public interface IImageService
{
    Task AddProductImageAsync(Guid productId, IFormFile imageFile);
}