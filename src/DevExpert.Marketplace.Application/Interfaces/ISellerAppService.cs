using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.Interfaces;

public interface ISellerAppService : IAppService<Seller, SellerInputViewModel, SellerOutputViewModel>;