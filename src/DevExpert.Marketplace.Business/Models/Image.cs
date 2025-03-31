using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Models;

public class Image : Entity
{
    public int DisplayPosition { get; set; }
    public string? Name { get; set; }
    public bool IsCover => DisplayPosition == 1;

    public Guid? ProductId { get; set; }
    public Product Product { get; set; }
}