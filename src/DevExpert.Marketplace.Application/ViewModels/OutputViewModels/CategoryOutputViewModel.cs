using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.ViewModels.OutputViewModels;

public class CategoryOutputViewModel : OutputViewModelBase<Category, CategoryOutputViewModel>
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }

    public override CategoryOutputViewModel FromModel(Category? model)
    {
        if (model == null)
            return null;

        return new CategoryOutputViewModel
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            AddedBy = model.AddedBy,
            AddedOn = model.AddedOn,
            ModifiedBy = model.ModifiedBy,
            ModifiedOn = model.ModifiedOn,
        };
    }
}