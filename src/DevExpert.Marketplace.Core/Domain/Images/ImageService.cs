using DevExpert.Marketplace.Core.Configurations;
using DevExpert.Marketplace.Core.Domain.Products;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Core.Domain.Images;

public class ImageService
{
    public static bool IsWebApi;
    private static Settings Settings => Settings.Instance!;

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
        var directoryPath = Combine(id);

        if (!Directory.Exists(directoryPath))
            return;

        Directory.Delete(directoryPath, true);
    }

    public static string Combine(Guid id, string name)
    {
        string path;
        if (IsWebApi)
        {
            path = Path.Combine(
                Settings.AppPath,
                Settings.RootPath,
                Settings.ProductImageDirectoryPath,
                id.ToString(),
                name);
        }

        path = Path.Combine(
            Settings.ProductImageDirectoryPath,
            id.ToString(),
            name);

        return NormalizePathSeparators(path);
    }

    private static async Task SaveAsync(INotifier notifier, Image image, IFormFile imageFile)
    {
        try
        {
            if (!imageFile.ContentType.StartsWith("image"))
            {
                notifier.AddNotification(new("O arquivo não é uma imagem."));
                return;
            }

            if (image.ProductId == Guid.Empty)
            {
                notifier.AddNotification(new("O Id do produto está vazio ou nulo."));
                return;
            }

            image.Name = $"{image.Id}{Path.GetExtension(imageFile.FileName)}";

            var directoryPath = Combine(image.ProductId.GetValueOrDefault());

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fullFilePath = NormalizePathSeparators(Path.Combine(directoryPath, image.Name!));

            await using var fileStream = new FileStream(fullFilePath, FileMode.Create);
            await imageFile.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            notifier.AddNotification(new($"Erro ao salvar a imagem: {ex.Message}"));
        }
    }

    private static string Combine(Guid id)
    {
        string path;

        if (IsWebApi)
        {
            path = Path.Combine(
                Settings.AppPath,
                Settings.RootPath,
                Settings.ProductImageDirectoryPath,
                id.ToString());
        }

        path = Path.Combine(
            Settings.RootPath,
            Settings.ProductImageDirectoryPath,
            id.ToString());

        return NormalizePathSeparators(path);
    }

    private static string NormalizePathSeparators(string path)
    {
        path = path.Replace("\\", "/");
        return path;
    }
}