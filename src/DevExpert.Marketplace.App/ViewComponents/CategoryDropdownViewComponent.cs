using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.ViewComponents;

public class CategoryDropdownViewComponent(ICategoryAppService appService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await appService.GetAllAsync();

        return View(categories.ToList());
    }
}