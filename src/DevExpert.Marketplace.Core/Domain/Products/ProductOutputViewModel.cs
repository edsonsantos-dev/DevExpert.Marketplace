using DevExpert.Marketplace.Core.Domain.Base;
using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Domain.Sellers;

namespace DevExpert.Marketplace.Core.Domain.Products;

public class ProductOutputViewModel : OutputViewModelBase
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public List<ImageOutputViewModel>? Images { get; private set; }
    public CategoryOutputViewModel? Category { get; private set; }
    public SellerOutputViewModel? Seller { get; private set; }

    public static ProductOutputViewModel FromModel(Product? model)
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
                .Select(ImageOutputViewModel.FromModel)
                .ToList(),
            Category = CategoryOutputViewModel.FromModel(model.Category),
            Seller = SellerOutputViewModel.FromModel(model.Seller),
            AddedBy = model.AddedBy,
            AddedOn = model.AddedOn,
            ModifiedBy = model.ModifiedBy,
            ModifiedOn = model.ModifiedOn,
        };
    }
}