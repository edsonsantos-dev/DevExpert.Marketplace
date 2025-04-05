using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.ViewComponents;

public class CategoryDropdownViewComponent(ICategoryAppService categoryAppService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid? selectedCategoryId = null)
    {
        var categories = await categoryAppService.GetAllAsync();
        
        ViewBag.SelectedCategoryId = selectedCategoryId;

        return View(categories.ToList());
    }
}