using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.InputViewModels;

public class SellerInputViewModel : InputViewModelBase<Seller>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public override Seller ToModel()
    {
        return new Seller
        {
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}