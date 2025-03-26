using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;

public class ImageOutputViewModel : OutputViewModelBase<Image, ImageOutputViewModel>
{
    public int DisplayPosition { get; private set; }
    public bool IsCover { get; private set; }
    public string? Path { get; private set; }

    public override ImageOutputViewModel FromModel(Image? entity)
    {
        if (entity == null)
            return null;

        return new ImageOutputViewModel
        {
            Id = entity.Id,
            DisplayPosition = entity.DisplayPosition,
            IsCover = entity.IsCover,
            Path = entity.Path,
            AddedOn = entity.AddedOn,
            AddedBy = entity.AddedBy
        };
    }
}