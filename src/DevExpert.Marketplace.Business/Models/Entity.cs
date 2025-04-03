using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Models;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime AddedOn { get; set; }
    public Guid AddedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }

    public virtual void Validation(INotifier notifier){}
}