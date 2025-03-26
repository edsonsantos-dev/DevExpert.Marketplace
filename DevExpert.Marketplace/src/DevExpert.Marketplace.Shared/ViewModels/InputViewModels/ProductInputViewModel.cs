using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.InputViewModels;

public class ProductInputViewModel : InputViewModelBase<Product>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public List<ImageInputViewModel> Images { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SellerId { get; set; }

    public override Product ToModel()
    {
        return new Product{
            Name = Name,
            Description = Description,
            Price = Price,
            Stock = Stock,
            CategoryId = CategoryId,
             SellerId = SellerId,
            Images = Images.Select(x => x.ToModel()).ToList()
        };
    }
}