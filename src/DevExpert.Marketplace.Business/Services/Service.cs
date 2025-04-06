using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Business.Services;

public abstract class Service<TEntity>(
    IRepository<TEntity> repository,
    INotifier notifier) : IService<TEntity> where TEntity : Entity
{
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.Validation(notifier);

        if (notifier.HaveNotification())
            return entity;

        entity = await repository.AddAsync(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.Validation(notifier);

        if (notifier.HaveNotification())
            return entity;

        entity = await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity == null)
        {
            notifier.AddNotification(new($"{typeof(TEntity).Name} não encontrado"));
            return;
        }

        await repository.DeleteAsync(entity);
        await repository.SaveChangesAsync();
    }

    public void Dispose()
    {
        repository?.Dispose();
    }
}