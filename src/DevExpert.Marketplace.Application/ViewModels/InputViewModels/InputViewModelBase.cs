using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.ViewModels.InputViewModels;

public abstract class InputViewModelBase<TEntity> : ViewModelBase where TEntity : Entity
{
    public abstract TEntity ToModel();
}