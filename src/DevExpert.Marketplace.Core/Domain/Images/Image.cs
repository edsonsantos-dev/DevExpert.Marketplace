using DevExpert.Marketplace.Core.Domain.Base;
using DevExpert.Marketplace.Core.Domain.Products;

namespace DevExpert.Marketplace.Core.Domain.Images;

public class Image : Entity
{
    public int DisplayPosition { get; set; }
    public string? Name { get; set; }
    public bool IsCover => DisplayPosition == 1;

    public Guid? ProductId { get; set; }
    public Product Product { get; set; }
}