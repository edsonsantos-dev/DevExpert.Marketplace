using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class DashboardController(
    ICategoryService categoryService,
    IProductService productService)
    : Controller
{
    [Route("dashboard")]
    public async Task<IActionResult> Index()
    {
        var categories = await categoryService.GetAllAsync();
        var products = await productService.GetProductsBySellerIdAsync();

        var model = Tuple.Create(categories.ToList(), products.ToList());

        return View(model);
    }
}