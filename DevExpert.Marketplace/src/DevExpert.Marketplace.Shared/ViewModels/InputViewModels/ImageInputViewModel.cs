using DevExpert.Marketplace.Business.Models;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Shared.ViewModels.InputViewModels;

public class ImageInputViewModel : InputViewModelBase<Image>
{
    public int DisplayPosition { get; set; }
    public Guid ProductId { get; set; }
    public IFormFile File { get; set; }

    public override Image ToModel()
    {
        return new Image
        {
            DisplayPosition = DisplayPosition,
            ProductId = ProductId
        };
    }
}