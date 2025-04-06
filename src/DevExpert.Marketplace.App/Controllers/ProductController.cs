using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class ProductController(
    IProductAppService appService,
    IUserContext userContext,
    INotifier notifier) : BaseController(notifier)
{
    [AllowAnonymous]
    [Route("lista-de-produtos")]
    public async Task<IActionResult> Index(List<Guid>? categories = null)
    {
        IEnumerable<ProductOutputViewModel> products;

        if (categories == null || categories.Count == 0)
            products = await appService.GetAllAsync();
        else
            products = await appService.GetProductsByCategoriesIdAsync(categories);

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

        if (!IsValid())
        {
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();

            return View(inputViewModel);
        }

        TempData["Success"] = "Produto cadastrada com sucesso!";

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

        var product = await appService.GetByIdAsync(inputViewModel.Id.GetValueOrDefault());
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        inputViewModel.SellerId = userContext.GetUserId();
        await appService.UpdateAsync(inputViewModel);

        if (!IsValid())
        {
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();

            return View(product);
        }

        TempData["Success"] = "Produto atualizado com sucesso!";

        return RedirectToAction("Index", "Dashboard");
    }

    [Route("excluir-produto/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await appService.DeleteAsync(id);
        
        if (!IsValid())
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();
        else
            TempData["Success"] = "Produto excluido com sucesso!";

        return RedirectToAction("Index", "Dashboard");
    }
}