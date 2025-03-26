using DevExpert.Marketplace.Application.ViewModels;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.InputViewModels;

public abstract class InputViewModelBase<TEntity> : ViewModelBase where TEntity : Entity
{
    public abstract TEntity ToModel();
}