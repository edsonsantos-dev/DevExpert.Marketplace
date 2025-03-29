using DevExpert.Marketplace.Business.Interfaces.Notifications;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Business.Models;

public class Image : Entity
{
    public string DirectoryPath => $"../DevExpert.Marketplace.App/wwwroot/images/products/{ProductId.ToString()}";

    public int DisplayPosition { get; set; }
    public string? Name { get; private set; }
    public bool IsCover => DisplayPosition == 1;

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public async Task SaveImageAsync(INotifier notifier, IFormFile file)
    {
        try
        {
            if (!file.ContentType.StartsWith("image"))
                notifier.AddNotification(new("File is not an image."));

            if (ProductId == Guid.Empty)
                notifier.AddNotification(new("Product Id is empty or null."));

            Name = $"{Id}{Path.GetExtension(file.FileName)}";

            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            var fullFilePath = Path.Combine(DirectoryPath, Name);

            await using var fileStream = new FileStream(fullFilePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            notifier.AddNotification(new($"Error saving image: {ex.Message}"));
        }
    }
}