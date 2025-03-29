using System.Net;
using DevExpert.Marketplace.Api.Controllers.Base;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Shared.ViewModels.InputViewModels;
using DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.Api.Controllers;

public class ProductController(INotifier notifier, IProductService service)
    : GenericController<ProductInputViewModel, Product, ProductOutputViewModel>(notifier, service)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsByCategoryIdAsync(Guid categoryId)
    {
        var products = await service.GetProductsByCategoryIdAsync(categoryId);

        return !products.Any()
            ? CustomResponse(HttpStatusCode.NoContent)
            : CustomResponse(HttpStatusCode.OK,
                products.Select(x => new ProductOutputViewModel().FromModel(x)));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsBySellerIdAsync(Guid sellerId)
    {
        var products = await service.GetProductsBySellerIdAsync(sellerId);

        return !products.Any()
            ? CustomResponse(HttpStatusCode.NoContent)
            : CustomResponse(HttpStatusCode.OK,
                products.Select(x => new ProductOutputViewModel().FromModel(x)));
    }

    [HttpPost("[action]")]
    public override async Task<IActionResult> AddAsync([FromForm] ProductInputViewModel inputViewModel)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var entity = inputViewModel.ToModel();

        await SaveProductImagesAsync(inputViewModel.Images, entity);

        if (notifier.HaveNotification()) return CustomResponse(HttpStatusCode.BadRequest);

        entity = await service.AddAsync(entity);

        return CustomResponse(HttpStatusCode.Created, new ProductOutputViewModel().FromModel(entity));
    }

    private async Task SaveProductImagesAsync(List<IFormFile> imagesFile, Product product)
    {
        var count = 1;
        foreach (var imageFile in imagesFile)
        {
            var image = new Image();
            image.DisplayPosition = count++;
            image.ProductId = product.Id;
            await image.SaveImageAsync(notifier, imageFile);
            product.Images.Add(image);
        }
    }
}