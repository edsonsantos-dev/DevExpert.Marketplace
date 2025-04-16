using DevExpert.Marketplace.Core.Domain.Base;
using DevExpert.Marketplace.Core.Domain.Products;
using DevExpert.Marketplace.Core.Notifications;

namespace DevExpert.Marketplace.Core.Domain.Categories;

public class Category : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<Product>? Products { get; set; }

    public override void Validation(INotifier notifier)
    {
        if (string.IsNullOrEmpty(Name))
            notifier.AddNotification(new($"Nome é obrigatório."));

        if (string.IsNullOrEmpty(Description))
            notifier.AddNotification(new($"Descrição é obrigatória."));
    }
    
    public bool CanBeDeleted(INotifier notifier)
    {
        if (Products?.Count == 0)
            return true;

        notifier.AddNotification(new($"Categoria não pode ser deletada porque possui produtos."));
        return false;
    }
}