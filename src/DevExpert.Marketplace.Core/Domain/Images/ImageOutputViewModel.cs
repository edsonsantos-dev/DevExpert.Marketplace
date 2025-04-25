using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Images;

public class ImageOutputViewModel : OutputViewModelBase
{
    public int DisplayPosition { get; private set; }
    public bool IsCover { get; private set; }
    public string? FilePath { get; set; }

    public static ImageOutputViewModel FromModel(Image? model)
    {
        if (model == null)
            return null;

        return new ImageOutputViewModel
        {
            Id = model.Id,
            DisplayPosition = model.DisplayPosition,
            IsCover = model.IsCover,
            FilePath = ImageService.Combine(
                model.ProductId.GetValueOrDefault(),
                model.Name),
            AddedBy = model.AddedBy,
            AddedOn = model.AddedOn,
            ModifiedBy = model.ModifiedBy,
            ModifiedOn = model.ModifiedOn,
        };
    }
}