namespace DevExpert.Marketplace.Core.Domain.Base;

public class OutputViewModelBase
{
    public Guid Id { get; protected set; }
    public DateTime AddedOn { get; protected set; }
    public Guid AddedBy { get; protected set; }
    public DateTime? ModifiedOn { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
}