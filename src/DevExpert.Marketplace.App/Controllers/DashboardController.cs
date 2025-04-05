using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

public class DashboardController(
    ICategoryAppService categoryService,
    IProductAppService productService,
    IUserContext userContext)
    : Controller
{
    [Route("dashboard")]
    public async Task<IActionResult> Index()
    {
        var categories = await categoryService.GetAllAsync();
        var products = await productService.GetProductsBySellerIdAsync(userContext.GetUserId());

        var model = Tuple.Create(categories.ToList(), products.ToList());

        return View(model);
    }
}