using DevExpert.Marketplace.Core.Configurations;
using DevExpert.Marketplace.Core.Domain.Products;
using DevExpert.Marketplace.Core.Domain.User;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Core.Domain.Images;

public class ImageService(
    IImageRepository repository,
    IUserContext userContext,
    INotifier notifier) : IImageService
{
    public static bool IsWebApi;
    private static Settings Settings => Settings.Instance!;

    public async Task DeleteAsync(Guid id)
    {
        var image = await repository.GetByIdAsync(id);

        if (image == null)
        {
            notifier.AddNotification(new Notification("Imagem não encontrada."));
            return;
        }

        if (image.AddedBy != userContext.GetUserId())
        {
            notifier.AddNotification(new Notification("A exclusão da imagem está restrita ao vendedor de origem."));
            return;
        }

        var images = await repository.GetImagesByProductIdAsync(image.ProductId.GetValueOrDefault());

        if (images.Count == 1)
        {
            notifier.AddNotification(new Notification("Primeiro, insira uma nova imagem."));
            return;
        }

        await repository.DeleteAsync(image);
        await repository.SaveChangesAsync();

        DeleteImage(image.ProductId.GetValueOrDefault(), image.Name);

        images.Remove(image);
        await ReorderProductImagesDisplayPositionAsync(images);
    }

    public async Task ReorderProductImagesDisplayPositionAsync(List<Image> images)
    {
        var count = 1;
        foreach (var image in images.OrderBy(x => x.DisplayPosition))
        {
            image.DisplayPosition = count++;
            await repository.UpdateAsync(image);
        }

        await repository.SaveChangesAsync();
    }

    public static async Task CreateImageAsync(Product product, List<IFormFile> imagesFile, INotifier notifier)
    {
        var count = 1;
        foreach (var imageFile in imagesFile)
        {
            var image = new Image
            {
                DisplayPosition = count++,
                ProductId = product.Id
            };

            await SaveAsync(notifier, image, imageFile);
            product.Images.Add(image);
        }
    }

    public static void DeleteImage(Guid productId, string? name = null)
    {
        var directoryPath = name == null ? Combine(productId) : Combine(productId, name);

        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, true);

        if (File.Exists(directoryPath))
            File.Delete(directoryPath);
    }

    public static string Combine(Guid id, string? name)
    {
        if (id == Guid.Empty || string.IsNullOrEmpty(name))
            return string.Empty;

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
        if (id == Guid.Empty)
            return string.Empty;

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