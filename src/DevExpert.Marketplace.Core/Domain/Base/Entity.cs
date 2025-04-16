using DevExpert.Marketplace.Core.Notifications;

namespace DevExpert.Marketplace.Core.Domain.Base;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime AddedOn { get; set; }
    public Guid AddedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }

    public Entity()
    {
        if (Id == Guid.Empty)
            Id = Guid.NewGuid();
    }
    
    public virtual void Validation(INotifier notifier){}
}