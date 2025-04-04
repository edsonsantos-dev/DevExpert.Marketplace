using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class CategoryController(ICategoryAppService appService) : Controller
{
    [Route("lista-de-categorias")]
    public async Task<IActionResult> Index()
    {
        var categories = await appService.GetAllAsync();

        return View(categories.ToList());
    }

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

        var outputViewModel = await appService.AddAsync(inputViewModel);

        return RedirectToAction(nameof(Index));
    }

    [Route("editar-categoria/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await appService.GetByIdAsync(id);

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

        return RedirectToAction(nameof(Index));
    }

    [Route("excluir-categoria/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await appService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}