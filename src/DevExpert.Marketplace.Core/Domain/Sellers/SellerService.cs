using DevExpert.Marketplace.Core.Notifications;

namespace DevExpert.Marketplace.Core.Domain.Sellers;

public class SellerService(ISellerRepository repository, INotifier notifier) : ISellerService
{
    public async Task<SellerOutputViewModel> AddAsync(SellerInputViewModel inputViewModel)
    {
        var seller = inputViewModel.ToModel();
        seller.Validation(notifier);

        if (notifier.HaveNotification())
            return SellerOutputViewModel.FromModel(seller);

        seller = await repository.AddAsync(seller);
        await repository.SaveChangesAsync();

        return SellerOutputViewModel.FromModel(seller);
    }

    public async Task<SellerOutputViewModel> GetAsync(Guid id)
    {
        var seller = await repository.GetByIdAsync(id);
        return SellerOutputViewModel.FromModel(seller);
    }

    public async Task<SellerOutputViewModel> UpdateAsync(SellerInputViewModel inputViewModel)
    {
        var seller = inputViewModel.ToModel();
        seller.Validation(notifier);

        if (notifier.HaveNotification())
            return SellerOutputViewModel.FromModel(seller);

        seller = await repository.UpdateAsync(seller);
        await repository.SaveChangesAsync();

        return SellerOutputViewModel.FromModel(seller);
    }

    public async Task DeleteAsync(Guid id)
    {
        var seller = await repository.GetByIdAsync(id);

        if (seller == null)
        {
            notifier.AddNotification(new Notification("Vendedor(a) não encontrado(a)."));
            return;
        }
        
        await repository.DeleteAsync(seller);
    }

    public void Dispose()
    {
        repository.Dispose();
    }
}