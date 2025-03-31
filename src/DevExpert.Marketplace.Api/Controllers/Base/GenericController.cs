using System.Net;
using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.Api.Controllers.Base;

public abstract class GenericController<TEntity, TInputViewModel, TOutputViewModel>(
    INotifier notifier,
    IAppService<TEntity, TInputViewModel, TOutputViewModel> appService) : BaseController(notifier)
    where TInputViewModel : InputViewModelBase<TEntity>
    where TEntity : Entity
    where TOutputViewModel : OutputViewModelBase<TEntity, TOutputViewModel>, new()
{
    [HttpPost("[action]")]
    public virtual async Task<IActionResult> AddAsync(TInputViewModel input)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var viewModel = await appService.AddAsync(input);

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }
    
    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var viewModel = await appService.GetByIdAsync(id);
        
        return viewModel == null 
            ? CustomResponse(HttpStatusCode.NotFound) 
            : CustomResponse(HttpStatusCode.OK, viewModel);
    }
    
    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        var viewModels = await appService.GetAllAsync();
        
        return !viewModels.Any() 
            ? CustomResponse(HttpStatusCode.NotFound) 
            : CustomResponse(HttpStatusCode.OK, viewModels);
    }    
    
    [HttpPut("[action]")]
    public virtual async Task<IActionResult> UpdateAsync(TInputViewModel input)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var viewModel = await appService.UpdateAsync(input);

        return CustomResponse(HttpStatusCode.OK, viewModel);
    }
    
    [HttpDelete("[action]")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        await appService.DeleteAsync(id);
        return CustomResponse();
    }
}