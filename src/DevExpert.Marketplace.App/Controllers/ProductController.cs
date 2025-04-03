using DevExpert.Marketplace.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

public class ProductController(IProductAppService appService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        var products = await appService.GetAllAsync();
        return View(products.ToList());
    }
}