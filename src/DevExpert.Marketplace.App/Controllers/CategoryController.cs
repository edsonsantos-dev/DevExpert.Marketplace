using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class CategoryController(ICategoryService appService, INotifier notifier) : BaseController(notifier)
{
    [Route("nova-categoria")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("nova-categoria")]
    [HttpPost]
    public async Task<IActionResult> Create(CategoryInputViewModel inputViewModel)
    {
        if (!ModelState.IsValid) return View(inputViewModel);

        await appService.AddAsync(inputViewModel);

        if (!IsValid())
        {
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();

            return View(inputViewModel);
        }

        TempData["Success"] = "Categoria cadastrada com sucesso!";

        return RedirectToAction("Index", "Dashboard");
    }

    [Route("editar-categoria/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await appService.GetAsync(id);

        if (category == null) return NotFound();

        return View(new CategoryInputViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        });
    }

    [Route("editar-categoria/{id:guid}")]
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CategoryInputViewModel inputViewModel)
    {
        if (id != inputViewModel.Id) return NotFound();
        if (!ModelState.IsValid) return View(inputViewModel);

        await appService.UpdateAsync(inputViewModel);

        if (!IsValid())
        {
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();

            return View(inputViewModel);
        }

        TempData["Success"] = "Categoria atualizada com sucesso!";

        return RedirectToAction("Index", "Dashboard");
    }

    [Route("excluir-categoria/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await appService.DeleteAsync(id);
        
        if (!IsValid())
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();
        else
            TempData["Success"] = "Categoria excluida com sucesso!";

        return RedirectToAction("Index", "Dashboard");
    }
}