using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.ViewModels.OutputViewModels;

public class ProductOutputViewModel : OutputViewModelBase<Product, ProductOutputViewModel>
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public List<ImageOutputViewModel>? Images { get; private set; }
    public CategoryOutputViewModel? Category { get; private set; }
    public SellerOutputViewModel? Seller { get; private set; }

    public override ProductOutputViewModel FromModel(Product? model)
    {
        if (model == null)
            return null;

        return new ProductOutputViewModel
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            Images = model.Images
                .Select(new ImageOutputViewModel().FromModel)
                .ToList(),
            Category = new CategoryOutputViewModel().FromModel(model.Category),
            Seller = new SellerOutputViewModel().FromModel(model.Seller),
            AddedBy = model.AddedBy,
            AddedOn = model.AddedOn,
            ModifiedBy = model.ModifiedBy,
            ModifiedOn = model.ModifiedOn,
        };
    }
}