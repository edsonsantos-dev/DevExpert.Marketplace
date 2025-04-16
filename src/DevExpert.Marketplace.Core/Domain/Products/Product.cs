using DevExpert.Marketplace.Core.Domain.Base;
using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Domain.Sellers;
using DevExpert.Marketplace.Core.Notifications;

namespace DevExpert.Marketplace.Core.Domain.Products;

public class Product : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public List<Image> Images { get; set; } = [];
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid SellerId { get; set; }
    public Seller? Seller { get; set; }

    public override void Validation(INotifier notifier)
    {
        if (string.IsNullOrEmpty(Name))
            notifier.AddNotification(new($"Nome é obrigatório."));

        if (string.IsNullOrEmpty(Description))
            notifier.AddNotification(new($"Descrição é obrigatória."));

        if (Stock <= 0)
            notifier.AddNotification(new($"O estoque deve ser maior que 0."));

        if (Price <= 0)
            notifier.AddNotification(new($"O preço deve ser maior que 0."));

        if (CategoryId == Guid.Empty)
            notifier.AddNotification(new($"A categoria é obrigatória."));

        if (SellerId == Guid.Empty)
            notifier.AddNotification(new($"O vendedor é obrigatório."));

        if (Images.Count == 0)
            notifier.AddNotification(new($"Imagens são obrigatórias."));
    }
}