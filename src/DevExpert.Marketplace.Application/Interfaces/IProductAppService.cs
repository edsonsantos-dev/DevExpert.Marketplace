using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.Interfaces;

public interface IProductAppService : IAppService<Product, ProductInputViewModel, ProductOutputViewModel>
{
    Task<List<ProductOutputViewModel>> GetProductsByCategoryIdAsync(Guid categoryId);
    Task<List<ProductOutputViewModel>> GetProductsBySellerIdAsync(Guid sellerId);
}