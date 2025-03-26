using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;

public class ImageOutputViewModel : OutputViewModelBase<Image, ImageOutputViewModel>
{
    public int DisplayPosition { get; private set; }
    public bool IsCover { get; private set; }
    public string? Path { get; private set; }

    public override ImageOutputViewModel FromModel(Image? model)
    {
        if (model == null)
            return null;

        return new ImageOutputViewModel
        {
            Id = model.Id,
            DisplayPosition = model.DisplayPosition,
            IsCover = model.IsCover,
            Path = model.Path,
            AddedBy = model.AddedBy,
            AddedOn = model.AddedOn,
            ModifiedBy = model.ModifiedBy,
            ModifiedOn = model.ModifiedOn,
        };
    }
}