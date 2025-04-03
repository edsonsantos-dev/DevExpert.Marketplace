using DevExpert.Marketplace.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

public class CategoryController(ICategoryAppService appService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        var categories = await appService.GetAllAsync();
        
        return View(categories.ToList());
    }
}