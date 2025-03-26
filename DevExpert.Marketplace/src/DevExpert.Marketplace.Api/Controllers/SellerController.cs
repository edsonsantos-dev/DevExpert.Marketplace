using DevExpert.Marketplace.Api.Controllers.Base;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Interfaces.Services;
using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Shared.ViewModels.InputViewModels;
using DevExpert.Marketplace.Shared.ViewModels.OutputViewModels;

namespace DevExpert.Marketplace.Api.Controllers;

public class SellerController(
    INotifier notifier,
    IService<Seller> service)
    : GenericController<SellerInputViewModel, Seller, SellerOutputViewModel>(notifier, service);