using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;

public class ProductOutputViewModel : OutputViewModelBase<Product, ProductOutputViewModel>
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public List<ImageOutputViewModel>? Images { get; private set; }
    public CategoryOutputViewModel? Category { get; private set; }
    public SellerOutputViewModel? Seller { get; private set; }

    public override ProductOutputViewModel FromModel(Product? entity)
    {
        if (entity == null)
            return null;

        return new ProductOutputViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Stock = entity.Stock,
            Images = entity.Images
                .Select(new ImageOutputViewModel().FromModel)
                .ToList(),
            Category = new CategoryOutputViewModel().FromModel(entity.Category),
            Seller = new SellerOutputViewModel().FromModel(entity.Seller),
            AddedOn = entity.AddedOn,
            AddedBy = entity.AddedBy,
            ModifiedOn = entity.ModifiedOn,
            ModifiedBy = entity.ModifiedBy
        };
    }
}