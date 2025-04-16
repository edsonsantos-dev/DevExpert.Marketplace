using DevExpert.Marketplace.Core.Domain.Base;
using DevExpert.Marketplace.Core.Domain.Products;

namespace DevExpert.Marketplace.Core.Domain.Sellers;

public class Seller : Entity
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public List<Product>? Products { get; set; }
}