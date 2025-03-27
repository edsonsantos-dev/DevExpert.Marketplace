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
}