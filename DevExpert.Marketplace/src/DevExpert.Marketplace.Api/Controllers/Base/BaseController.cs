using System.Net;
using DevExpert.Marketplace.Business.Interfaces.Notifications;
using DevExpert.Marketplace.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevExpert.Marketplace.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public abstract class BaseController(INotifier notifier) : ControllerBase
{
    private bool IsValid()
    {
        return !notifier.HaveNotification();
    }

    protected IActionResult CustomResponse(
        HttpStatusCode statusCode = HttpStatusCode.OK,
        object result = null)
    {
        if (IsValid())
            return new ObjectResult(result)
            {
                StatusCode = Convert.ToInt32(statusCode)
            };

        return BadRequest(new
        {
            Errors = notifier.GetNotifications().Select(x => x.Message)
        });
    }

    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorInvalidModel(modelState);
        return CustomResponse();
    }

    private void NotifyErrorInvalidModel(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            var message = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(message);
        }
    }

    protected void NotifyError(string message)
    {
        notifier.AddNotification(new Notification(message));
    }
}