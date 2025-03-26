using DevExpert.Marketplace.Business.Interfaces.Repositories;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly MarketplaceContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(MarketplaceContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        entity = (await DbSet.AddAsync(entity)).Entity;
        return entity;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity = DbSet.Update(entity).Entity;
        return await Task.FromResult(entity);
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}