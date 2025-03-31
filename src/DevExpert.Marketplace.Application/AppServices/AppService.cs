using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.AppServices;

public class AppService<TEntity, TInputViewModel, TOutputViewModel>(IService<TEntity> service)
    : IAppService<TEntity, TInputViewModel, TOutputViewModel>
    where TEntity : Entity
    where TInputViewModel : InputViewModelBase<TEntity>
    where TOutputViewModel : OutputViewModelBase<TEntity, TOutputViewModel>, new()
{
    public virtual async Task<TOutputViewModel> AddAsync(TInputViewModel inputViewModel)
    {
        var entity = inputViewModel.ToModel();
        entity = await service.AddAsync(entity);

        return new TOutputViewModel().FromModel(entity);
    }

    public virtual async Task<TOutputViewModel?> GetByIdAsync(Guid id)
    {
        var entity = await service.GetByIdAsync(id);
        return new TOutputViewModel().FromModel(entity);
    }

    public virtual async Task<IEnumerable<TOutputViewModel>> GetAllAsync()
    {
        var entities = await service.GetAllAsync();
        return entities.Select(e => new TOutputViewModel().FromModel(e));
    }

    public virtual async Task<TOutputViewModel> UpdateAsync(TInputViewModel inputViewModel)
    {
        var entity = inputViewModel.ToModel();
        entity = await service.AddAsync(entity);

        return new TOutputViewModel().FromModel(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await service.DeleteAsync(id);
    }

    public void Dispose()
    {
        service?.Dispose();
    }
}