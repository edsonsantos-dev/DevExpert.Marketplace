using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.AppServices;

public class SellerAppService(IService<Seller> service)
    : AppService<Seller, SellerInputViewModel, SellerOutputViewModel>(service);