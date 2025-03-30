using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.Interfaces;

public interface IAppService<TEntity, TInputViewModel, TOutputViewModel> : IDisposable
    where TEntity : Entity
    where TInputViewModel : InputViewModelBase<TEntity>
    where TOutputViewModel : OutputViewModelBase<TEntity, TOutputViewModel>
{
    Task<TOutputViewModel> AddAsync(TInputViewModel inputViewModel);
    Task<TOutputViewModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<TOutputViewModel>> GetAllAsync();
    Task<TOutputViewModel> UpdateAsync(TInputViewModel inputViewModel);
    Task DeleteAsync(Guid id);
}