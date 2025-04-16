using DevExpert.Marketplace.Core.Domain.Categories;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.ViewComponents;

public class CategoryDropdownViewComponent(ICategoryService categoryAppService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid? selectedCategoryId = null)
    {
        var categories = await categoryAppService.GetAllAsync();
        
        ViewBag.SelectedCategoryId = selectedCategoryId;

        return View(categories.ToList());
    }
}