using DevExpert.Marketplace.Core.Notifications;

namespace DevExpert.Marketplace.Core.Domain.Categories;

public class CategoryService(ICategoryRepository repository, INotifier notifier) : ICategoryService
{
    public async Task<CategoryOutputViewModel> AddAsync(CategoryInputViewModel inputViewModel)
    {
        var category = inputViewModel.ToModel();
        category.Validation(notifier);

        if (notifier.HaveNotification())
            return CategoryOutputViewModel.FromModel(category);

        category = await repository.AddAsync(category);
        await repository.SaveChangesAsync();

        return CategoryOutputViewModel.FromModel(category);
    }

    public async Task<CategoryOutputViewModel> GetAsync(Guid id)
    {
        var category = await repository.GetByIdAsync(id);
        return CategoryOutputViewModel.FromModel(category);
    }

    public async Task<List<CategoryOutputViewModel>> GetAllAsync()
    {
        var categories = await repository.GetAllAsync();
        return categories.Select(CategoryOutputViewModel.FromModel).ToList();
    }

    public async Task<CategoryOutputViewModel> UpdateAsync(CategoryInputViewModel inputViewModel)
    {
        var category = inputViewModel.ToModel();
        category.Validation(notifier);

        if (notifier.HaveNotification())
            return CategoryOutputViewModel.FromModel(category);

        category = await repository.UpdateAsync(category);
        await repository.SaveChangesAsync();

        return CategoryOutputViewModel.FromModel(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await repository.GetCategoryWithProductsAsync(id);

        if (category == null)
        {
            notifier.AddNotification(new("Categoria não encontrada."));
            return;
        }

        if (category.CanBeDeleted(notifier))
        {
            await repository.DeleteAsync(category);
            await repository.SaveChangesAsync();
        }
    }

    public void Dispose()
    {
        repository.Dispose();
    }
}