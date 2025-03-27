using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Models;

public class Image : Entity
{
    public int DisplayPosition { get; set; }
    public string? FilePath { get; set; }
    public string? Name { get; private set; }
    public bool IsCover => DisplayPosition == 1;
    public string FileBase64 { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public bool SaveImage(INotifier notifier, Guid productId)
    {
        try
        {
            if (productId == Guid.Empty)
            {
                notifier.AddNotification(new("Product Id is empty or null."));
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(FileBase64))
            {
                notifier.AddNotification(new("FileBase64 is empty or null."));
                return false;
            }

            ProductId = productId;
            Name = $"{Id}.png";
            FilePath = Path.Combine("images", "products", ProductId.ToString(), Name);

            var webRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "uploads");
            var fullDirectoryPath = Path.Combine(webRootPath, "images", "products", ProductId.ToString());

            if (!Directory.Exists(fullDirectoryPath))
                Directory.CreateDirectory(fullDirectoryPath);

            var fullFilePath = Path.Combine(fullDirectoryPath, Name);

            var imageBytes = ConvertBase64ToImage(notifier);
            File.WriteAllBytes(fullFilePath, imageBytes);
            FilePath = fullFilePath;

            return true;
        }
        catch (Exception ex)
        {
            notifier.AddNotification(new($"Error saving image: {ex.Message}"));
            return false;
        }
    }

    private byte[] ConvertBase64ToImage(INotifier notifier)
    {
        try
        {
            return Convert.FromBase64String(FileBase64);
        }
        catch (FormatException)
        {
            notifier.AddNotification(new("Invalid Base64 format."));
            return [];
        }
    }
}
