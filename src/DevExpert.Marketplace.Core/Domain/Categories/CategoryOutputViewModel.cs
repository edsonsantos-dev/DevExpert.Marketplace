using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Categories;

public class CategoryOutputViewModel : OutputViewModelBase
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }

    public static CategoryOutputViewModel FromModel(Category? model)
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