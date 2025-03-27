using System.Net;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Shared.ViewModels.InputViewModels;
using DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.Api.Controllers.Base;

public abstract class GenericController<
    TInputViewModel,
    TEntity,
    TOutputViewModel>(
    INotifier notifier,
    IService<TEntity> service) : BaseController(notifier)
    where TInputViewModel : InputViewModelBase<TEntity>
    where TEntity : Entity
    where TOutputViewModel : OutputViewModelBase<TEntity, TOutputViewModel>, new()
{
    [HttpPost("[action]")]
    public virtual async Task<IActionResult> AddAsync(TInputViewModel inputViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var entity = inputViewModel.ToModel();
        
        entity = await service.AddAsync(entity);

        return CustomResponse(HttpStatusCode.Created, new TOutputViewModel().FromModel(entity));
    }

    [HttpPut("[action]")]
    public virtual async Task<IActionResult> UpdateAsync(TInputViewModel inputViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        var entity = inputViewModel.ToModel();
        
        entity = await service.UpdateAsync(entity);

        return CustomResponse(HttpStatusCode.OK, new TOutputViewModel().FromModel(entity));
    }

    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var entity = await service.GetByIdAsync(id);

        if (entity == null) return CustomResponse(HttpStatusCode.NoContent);

        return CustomResponse(HttpStatusCode.OK, new TOutputViewModel().FromModel(entity));
    }

    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        var entities = await service.GetAllAsync();

        if (!entities.Any()) return CustomResponse(HttpStatusCode.NoContent);

        return CustomResponse(
            HttpStatusCode.OK,
                entities.Select(x => 
                    new TOutputViewModel().FromModel(x)));
    }

    [HttpDelete("[action]")]
    public virtual async Task<IActionResult> RemoveAsync(Guid id)
    {
        await service.DeleteAsync(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }
}