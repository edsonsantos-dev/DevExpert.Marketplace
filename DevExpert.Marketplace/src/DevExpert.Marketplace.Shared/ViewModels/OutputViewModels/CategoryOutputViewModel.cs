using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;

public class CategoryOutputViewModel : OutputViewModelBase<Category, CategoryOutputViewModel>
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }

    public List<ProductOutputViewModel>? Products { get; private set; }

    public override CategoryOutputViewModel FromModel(Category? entity)
    {
        if (entity == null)
            return null;

        return new CategoryOutputViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Products = entity.Products
                .Select(new ProductOutputViewModel().FromModel)
                .ToList(),
        };
    }
}