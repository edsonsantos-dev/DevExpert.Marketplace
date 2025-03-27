using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.InputViewModels;

public class ImageInputViewModel : InputViewModelBase<Image>
{
    public int DisplayPosition { get; set; }
    public string FileBase64 { get; set; }
    public string? FilePath { get; set; }

    public override Image ToModel()
    {
        return new Image
        {
            DisplayPosition = DisplayPosition,
            FilePath = FilePath,
            FileBase64 = FileBase64
        };
    }
}