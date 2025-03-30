using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Models;

public class Category : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<Product>? Products { get; set; }

    public override void Validation(INotifier notifier)
    {
        if (string.IsNullOrEmpty(Name))
            notifier.AddNotification(new($"Name is required."));

        if (string.IsNullOrEmpty(Description))
            notifier.AddNotification(new($"Description is required."));
    }
    
    public bool CanBeDeleted(INotifier notifier)
    {
        if (Products?.Count == 0)
            return true;

        notifier.AddNotification(new($"Category can not be deleted because it has products."));
        return false;
    }
}