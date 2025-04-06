using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Models;

public class Seller : Entity
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public List<Product>? Products { get; set; }

    public override void Validation(INotifier notifier)
    {
        if (string.IsNullOrEmpty(FullName))
            notifier.AddNotification(new($"Nome completo é obrigatório."));

        if (string.IsNullOrEmpty(Email))
            notifier.AddNotification(new($"E-mail é obrigatório."));
    }
}