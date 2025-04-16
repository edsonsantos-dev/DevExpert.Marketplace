using DevExpert.Marketplace.Core.Domain.Categories;
using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Domain.Products;
using DevExpert.Marketplace.Core.Domain.Sellers;
using DevExpert.Marketplace.Core.Domain.User;
using DevExpert.Marketplace.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DevExpert.Marketplace.Core.Data.Context;

public sealed class MarketplaceContext : DbContext
{
    private readonly IUserContext _userContext;

    public Guid UserId => _userContext.GetUserId();
    
    public MarketplaceContext(
        DbContextOptions<MarketplaceContext> options,
        IUserContext userContext) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        _userContext = userContext;
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var mutableProperties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string)));

        foreach (var property in mutableProperties)
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketplaceContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        HandleAddedOnAndAddedByForEntities();
        HandleModifiedOnAndModifiedByForEntities();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    private void HandleModifiedOnAndModifiedByForEntities()
    {
        var entityEntries = ChangeTracker
            .Entries()
            .Where(x => x.Entity.GetType().GetProperty("ModifiedOn") != null ||
                        x.Entity.GetType().GetProperty("ModifiedBy") != null);

        foreach (var entityEntry in entityEntries)
        {
            if (entityEntry.State != EntityState.Modified) continue;

            entityEntry.Property("ModifiedOn").CurrentValue = DateTime.Now;
            entityEntry.Property("ModifiedBy").CurrentValue = UserId;
        }
    }

    private void HandleAddedOnAndAddedByForEntities()
    {
        var entityEntries = ChangeTracker.Entries()
            .Where(x => x.Entity.GetType().GetProperty("AddedOn") != null ||
                        x.Entity.GetType().GetProperty("AddedBy") != null);

        foreach (var entityEntry in entityEntries)
        {
            var isAddedState = entityEntry.State == EntityState.Added;
            var isModifiedState = entityEntry.State == EntityState.Modified;

            SetPropertyIfNotNull(entityEntry, "AddedOn", DateTime.Now, isAddedState, isModifiedState);
            SetPropertyIfNotNull(entityEntry, "AddedBy", UserId, isAddedState, isModifiedState);
        }
    }
    
    private static void SetPropertyIfNotNull(
        EntityEntry entityEntry, 
        string propertyName, 
        object newValue, 
        bool changeValueIfAddedState, 
        bool changeIsModifiedIfModifiedState, 
        Guid? condition = null)
    {
        var property = entityEntry.Metadata.FindProperty(propertyName);

        if (property == null) return;
        
        if (changeValueIfAddedState && (condition == null || (Guid)entityEntry.Property(propertyName).CurrentValue! == condition.Value))
            entityEntry.Property(propertyName).CurrentValue = newValue;

        if (changeIsModifiedIfModifiedState)
            entityEntry.Property(propertyName).IsModified = false;
    }
}