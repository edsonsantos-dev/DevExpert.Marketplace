using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.InputViewModels;

public class CategoryInputViewModel : InputViewModelBase<Category>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public override Category ToModel()
    {
        return new Category
        {
            Name = Name,
            Description = Description
        };
    }
}