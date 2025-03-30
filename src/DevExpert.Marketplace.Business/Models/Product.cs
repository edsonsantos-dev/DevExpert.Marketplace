using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Models;

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
            notifier.AddNotification(new($"Name is required."));

        if (string.IsNullOrEmpty(Description))
            notifier.AddNotification(new($"Description is required."));

        if (Stock <= 0)
            notifier.AddNotification(new($"Stock must be greater than 0."));

        if (Price <= 0)
            notifier.AddNotification(new($"Price must be greater than 0."));

        if (CategoryId == Guid.Empty)
            notifier.AddNotification(new($"Category is required."));

        if (SellerId == Guid.Empty)
            notifier.AddNotification(new($"Seller is required."));

        if (Images.Count == 0)
            notifier.AddNotification(new($"Images is required."));
    }
}