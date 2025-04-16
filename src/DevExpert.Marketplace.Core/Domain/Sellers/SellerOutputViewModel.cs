using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Sellers;

public class SellerOutputViewModel : OutputViewModelBase
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public static SellerOutputViewModel FromModel(Seller? model)
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