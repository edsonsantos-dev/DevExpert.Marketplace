using DevExpert.Marketplace.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.ViewComponents;

public class CategoryFilterViewComponent(ICategoryAppService appService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await appService.GetAllAsync();
        return View(categories.ToList());
    }
}