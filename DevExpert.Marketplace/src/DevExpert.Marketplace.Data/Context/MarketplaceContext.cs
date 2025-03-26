using DevExpert.Marketplace.Business.Interfaces;
using DevExpert.Marketplace.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.Data.Context;

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

        var mutableForeignKeys = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e =>
                e.GetForeignKeys());

        foreach (var mutableForeignKey in mutableForeignKeys)
            mutableForeignKey.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        HandleAddedFieldsForEntities();
        HandleModifiedFieldsForEntities();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void HandleAddedFieldsForEntities()
    {
        HandleAuditFieldsForEntities(
            EntityState.Modified,
            new Dictionary<string, object>
            {
                { "AddedOn", DateTime.UtcNow },
                { "AddedBy", UserId }
            });
    }

    private void HandleModifiedFieldsForEntities()
    {
        HandleAuditFieldsForEntities(
            EntityState.Modified,
            new Dictionary<string, object>
            {
                { "ModifiedOn", DateTime.UtcNow },
                { "ModifiedBy", UserId }
            });
    }

    private void HandleAuditFieldsForEntities(
        EntityState applicableState,
        Dictionary<string, object> properties)
    {
        var entityEntries = ChangeTracker.Entries()
            .Where(x =>
                properties.Keys.Any(prop =>
                    x.Entity.GetType().GetProperty(prop) != null));

        foreach (var entityEntry in entityEntries)
        {
            if (entityEntry.State != applicableState) continue;

            foreach (var property in properties)
                entityEntry.Property(property.Key).CurrentValue = property.Value;
        }
    }
}