using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.ViewModels.OutputViewModels;

public abstract class OutputViewModelBase<TEntity, TOutputViewModel> : ViewModelBase 
    where TEntity : Entity
    where TOutputViewModel : ViewModelBase
{
    public DateTime AddedOn { get; protected set; }
    public Guid AddedBy { get; protected set; }
    public DateTime? ModifiedOn { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    public abstract TOutputViewModel FromModel(TEntity model);
}