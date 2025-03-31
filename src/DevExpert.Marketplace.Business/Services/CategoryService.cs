using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Services;

public class CategoryService(
    ICategoryRepository repository,
    INotifier notifier) : Service<Category>(repository, notifier)
{
    public override async Task DeleteAsync(Guid id)
    {
        var category = await repository.GetCategoryWithProductsAsync(id);

        if (category == null)
        {
            notifier.AddNotification(new("Category not found."));
            return;
        }

        if (category.CanBeDeleted(notifier))
        {
            await repository.DeleteAsync(category);
            await repository.SaveChangesAsync();
        }
    }
}