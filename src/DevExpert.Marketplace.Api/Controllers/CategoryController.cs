using System.Net;
using DevExpert.Marketplace.Api.Controllers.Base;
using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.Api.Controllers;

public class CategoryController(INotifier notifier, ICategoryService service)
    : BaseController(notifier)
{
    [HttpPost("[action]")]
    public async Task<IActionResult> AddAsync(CategoryInputViewModel input)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var viewModel = await service.AddAsync(input);

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var viewModel = await service.GetAsync(id);

        return viewModel == null
            ? CustomResponse(HttpStatusCode.NotFound)
            : CustomResponse(HttpStatusCode.OK, viewModel);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        var viewModels = await service.GetAllAsync();

        return !viewModels.Any()
            ? CustomResponse(HttpStatusCode.NotFound)
            : CustomResponse(HttpStatusCode.OK, viewModels);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateAsync(CategoryInputViewModel input)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var viewModel = await service.UpdateAsync(input);

        return CustomResponse(HttpStatusCode.OK, viewModel);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await service.DeleteAsync(id);
        return CustomResponse();
    }
}