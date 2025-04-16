using DevExpert.Marketplace.Core.Domain.Categories;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.ViewComponents;

public class CategoryFilterViewComponent(ICategoryService appService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await appService.GetAllAsync();
        return View(categories.ToList());
    }
}