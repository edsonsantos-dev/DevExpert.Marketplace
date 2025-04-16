using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

public class BaseController(INotifier notifier) : Controller
{
    protected bool IsValid() => !notifier.HaveNotification();
}