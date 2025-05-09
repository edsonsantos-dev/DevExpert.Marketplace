using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Domain.Products;
using DevExpert.Marketplace.Core.Domain.User;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class ProductController(
    IProductService service,
    IImageService imageService,
    IUserContext userContext,
    INotifier notifier) : BaseController(notifier)
{
    [AllowAnonymous]
    [Route("lista-de-produtos")]
    public async Task<IActionResult> Index(List<Guid>? categories = null)
    {
        IEnumerable<ProductOutputViewModel> products;

        if (categories == null || categories.Count == 0)
            products = await service.GetAllAsync();
        else
            products = await service.GetProductsByCategoriesIdAsync(categories);

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

        await service.AddAsync(inputViewModel);

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
        var product = await service.GetAsync(id);
        return View(product);
    }

    [Route("editar-produto/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await service.GetAsync(id);

        if (product == null) return NotFound();

        return View(product);
    }

    [Route("editar-produto/{id:guid}")]
    [HttpPost]
    public async Task<IActionResult> Edit(ProductInputViewModel inputViewModel)
    {
        var hasImage = await service.ProductHasImageAsync(inputViewModel.Id.GetValueOrDefault());

        if (hasImage)
            ModelState.Remove("Images");

        var product = await service.GetAsync(inputViewModel.Id.GetValueOrDefault());
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        inputViewModel.SellerId = userContext.GetUserId();
        await service.UpdateAsync(inputViewModel);

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
        await service.DeleteAsync(id);
        
        if (!IsValid())
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();
        else
            TempData["Success"] = "Produto excluido com sucesso!";

        return RedirectToAction("Index", "Dashboard");
    }
    
    [Route("excluir-imagem/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        await imageService.DeleteAsync(id);

        if (!IsValid())
        {
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();
            return BadRequest();
        }

        TempData["Success"] = "Imagem excluida com sucesso!";

        return Ok();
    }
}