using DevExpert.Marketplace.Api.Controllers.Base;
using DevExpert.Marketplace.Application.Interfaces;
using DevExpert.Marketplace.Application.ViewModels.InputViewModels;
using DevExpert.Marketplace.Application.ViewModels.OutputViewModels;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Api.Controllers;

public class CategoryController(INotifier notifier, ICategoryAppService appService) 
    : GenericController<Category, CategoryInputViewModel, CategoryOutputViewModel>(notifier, appService);