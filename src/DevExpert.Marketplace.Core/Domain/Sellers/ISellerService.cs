namespace DevExpert.Marketplace.Core.Domain.Sellers;

public interface ISellerService : IDisposable
{
    Task<SellerOutputViewModel> AddAsync(SellerInputViewModel inputViewModel);
    Task<SellerOutputViewModel> GetAsync(Guid id);
    Task<SellerOutputViewModel> UpdateAsync(SellerInputViewModel inputViewModel);
    Task DeleteAsync(Guid id);
}