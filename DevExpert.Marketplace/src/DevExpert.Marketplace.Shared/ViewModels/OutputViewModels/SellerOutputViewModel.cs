using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;

public class SellerOutputViewModel : OutputViewModelBase<Seller, SellerOutputViewModel>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public override SellerOutputViewModel FromModel(Seller? entity)
    {
        if (entity == null)
            return null;

        return new SellerOutputViewModel
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            AddedOn = entity.AddedOn,
            AddedBy = entity.AddedBy,
            ModifiedOn = entity.ModifiedOn,
            ModifiedBy = entity.ModifiedBy,
        };
    }
}