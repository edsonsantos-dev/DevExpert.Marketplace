using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Interfaces.Services;

public interface IService<TEntity> : IDisposable where TEntity : Entity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}