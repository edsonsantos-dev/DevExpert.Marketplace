using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.AppServices;

public class SellerAppService(
    IService<Seller> service,
    INotifier notifier)
    : AppService<Seller, SellerInputViewModel, SellerOutputViewModel>(service), ISellerAppService
{
    public override async Task<SellerOutputViewModel> AddAsync(SellerInputViewModel inputViewModel)
    {
        var seller = inputViewModel.ToModel();

        if (!inputViewModel.Id.HasValue)
        {
            notifier.AddNotification(new("Id is required"));
            return null;
        }
        
        seller.Id = inputViewModel.Id ?? Guid.NewGuid();
        seller = await service.AddAsync(seller);
        
        return new SellerOutputViewModel().FromModel(seller);
    }
}