using DevExpert.Marketplace.Core.Domain.Images;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

[Authorize]
public class ImageController(IImageService service, INotifier notifier) : BaseController(notifier)
{
    [Route("excluir-imagem/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeleteAsync(id);

        if (!IsValid())
        {
            TempData["Error"] = notifier.GetNotifications().Select(x => x.Message).FirstOrDefault();
            return BadRequest();
        }

        TempData["Success"] = "Imagem excluida com sucesso!";

        return Ok();
    }
}