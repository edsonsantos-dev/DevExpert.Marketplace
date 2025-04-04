using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

public class ProductController(IProductAppService appService) : Controller
{
    [Route("lista-de-produtos")]
    public async Task<IActionResult> Index()
    {
        var products = await appService.GetAllAsync();
        return View(products.ToList());
    }
    
    [Route("novo-produto")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("novo-produto")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductInputViewModel inputViewModel)
    {
        if (!ModelState.IsValid) return View(inputViewModel);

        var outputViewModel = await appService.AddAsync(inputViewModel);

        return RedirectToAction(nameof(Index));
    }
}