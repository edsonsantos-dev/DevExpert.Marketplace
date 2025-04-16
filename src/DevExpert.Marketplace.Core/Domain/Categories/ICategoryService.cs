namespace DevExpert.Marketplace.Core.Domain.Categories;

public interface ICategoryService : IDisposable
{
    Task<CategoryOutputViewModel> AddAsync(CategoryInputViewModel inputViewModel);
    Task<CategoryOutputViewModel> GetAsync(Guid id);
    Task<List<CategoryOutputViewModel>> GetAllAsync();
    Task<CategoryOutputViewModel> UpdateAsync(CategoryInputViewModel inputViewModel);
    Task DeleteAsync(Guid id);
}