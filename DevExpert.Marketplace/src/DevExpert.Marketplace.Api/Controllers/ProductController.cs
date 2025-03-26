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
    public async Task<IActionResult> GetProductByCategoryIdAsync(Guid categoryId)
    {
        var product = await service.GetProductByCategoryIdAsync(categoryId);

        return product == null
            ? CustomResponse(HttpStatusCode.NoContent)
            : CustomResponse(HttpStatusCode.OK, new ProductOutputViewModel().FromModel(product));
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductBySellerIdAsync(Guid sellerId)
    {
        var product = await service.GetProductBySellerIdAsync(sellerId);

        return product == null
            ? CustomResponse(HttpStatusCode.NoContent)
            : CustomResponse(HttpStatusCode.OK, new ProductOutputViewModel().FromModel(product));
    }
}