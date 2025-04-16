using System.Net;
using DevExpert.Marketplace.Api.Controllers.Base;
using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.Api.Controllers;

public class ProductController(INotifier notifier, IProductAppService appService)
    : GenericController<Product, ProductInputViewModel, ProductOutputViewModel>(notifier, appService)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsByCategoriesIdAsync(List<Guid> categoriesId)
    {
        var products = await appService.GetProductsByCategoriesIdAsync(categoriesId);

        return products.Count == 0
            ? CustomResponse(HttpStatusCode.NoContent)
            : CustomResponse(HttpStatusCode.OK, products);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsBySellerIdAsync(Guid sellerId)
    {
        var products = await appService.GetProductsBySellerIdAsync(sellerId);

        return products.Count == 0
            ? CustomResponse(HttpStatusCode.NoContent)
            : CustomResponse(HttpStatusCode.OK, products);
    }
}