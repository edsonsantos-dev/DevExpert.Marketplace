using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Models;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Application.Helpers;

public static class ImageHelper
{
    public static async Task CreateImageAsync(Product product, List<IFormFile> imagesFile, INotifier notifier)
    {
        var count = 1;
        foreach (var imageFile in imagesFile)
        {
            var image = new Image();
            image.DisplayPosition = count++;
            image.ProductId = product.Id;
            await SaveAsync(notifier, image, imageFile);
            product.Images.Add(image);
        }
    }

    public static void DeleteImage(Guid id)
    {
        var directoryPath = Path.Combine(Settings.Instance?.ProductImageDirectoryPath!, id.ToString());

        if (!Directory.Exists(directoryPath))
            return;

        Directory.Delete(directoryPath, true);
    }

    private static async Task SaveAsync(INotifier notifier, Image image, IFormFile imageFile)
    {
        try
        {
            if (!imageFile.ContentType.StartsWith("image"))
            {
                notifier.AddNotification(new("File is not an image."));
                return;
            }

            if (image.ProductId == Guid.Empty)
            {
                notifier.AddNotification(new("Product Id is empty or null."));
                return;
            }

            image.Name = $"{image.Id}{Path.GetExtension(imageFile.FileName)}";

            var directoryPath =
                Path.Combine(Settings.Instance?.ProductImageDirectoryPath!, image.ProductId.ToString()!);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fullFilePath = Path.Combine(directoryPath, image.Name!);

            await using var fileStream = new FileStream(fullFilePath, FileMode.Create);
            await imageFile.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            notifier.AddNotification(new($"Error saving image: {ex.Message}"));
        }
    }
}