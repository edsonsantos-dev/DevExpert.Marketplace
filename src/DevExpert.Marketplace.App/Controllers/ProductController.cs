using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class ProductController(
    IProductAppService appService,
    IUserContext userContext) : Controller
{
    [AllowAnonymous]
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

        inputViewModel.SellerId = userContext.GetUserId();

        await appService.AddAsync(inputViewModel);

        return RedirectToAction("Index", "Dashboard");
    }

    [AllowAnonymous]
    [Route("detalhes-do-produto/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await appService.GetByIdAsync(id);
        return View(product);
    }

    [Route("editar-produto/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await appService.GetByIdAsync(id);

        if (product == null) return NotFound();

        return View(product);
    }

    [Route("editar-produto/{id:guid}")]
    [HttpPost]
    public async Task<IActionResult> Edit(ProductInputViewModel inputViewModel)
    {
        var hasImage = await appService.ProductHasImageAsync(inputViewModel.Id.GetValueOrDefault());

        if (hasImage)
            ModelState.Remove("Images");

        if (!ModelState.IsValid)
        {
            var product = await appService.GetByIdAsync(inputViewModel.Id.GetValueOrDefault());
            return View(product);
        }

        inputViewModel.SellerId = userContext.GetUserId();
        await appService.UpdateAsync(inputViewModel);

        return RedirectToAction("Index", "Dashboard");
    }
}