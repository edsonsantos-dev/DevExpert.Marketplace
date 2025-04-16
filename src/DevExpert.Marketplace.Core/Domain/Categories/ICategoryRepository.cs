using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetCategoryWithProductsAsync(Guid id);
}