using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.ViewModels.OutputViewModels;

public class SellerOutputViewModel : OutputViewModelBase<Seller, SellerOutputViewModel>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public override SellerOutputViewModel FromModel(Seller? model)
    {
        if (model == null)
            return null;

        return new SellerOutputViewModel
        {
            Id = model.Id,
            FullName = model.FullName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            AddedBy = model.AddedBy,
            AddedOn = model.AddedOn,
            ModifiedBy = model.ModifiedBy,
            ModifiedOn = model.ModifiedOn,
        };
    }
}