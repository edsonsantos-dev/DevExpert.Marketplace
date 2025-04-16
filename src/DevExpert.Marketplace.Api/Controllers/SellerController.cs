using System.Net;
using DevExpert.Marketplace.Api.Controllers.Base;
using DevExpert.Marketplace.Core.Domain.Sellers;
using DevExpert.Marketplace.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.Api.Controllers;

public class SellerController(INotifier notifier, ISellerService service) : BaseController(notifier)
{
    [HttpPost("[action]")]
    public virtual async Task<IActionResult> AddAsync(SellerInputViewModel input)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var viewModel = await service.AddAsync(input);

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }
    
    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetAsync(Guid id)
    {
        var viewModel = await service.GetAsync(id);
        
        return viewModel == null 
            ? CustomResponse(HttpStatusCode.NotFound) 
            : CustomResponse(HttpStatusCode.OK, viewModel);
    }
    
    [HttpPut("[action]")]
    public virtual async Task<IActionResult> UpdateAsync(SellerInputViewModel input)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var viewModel = await service.UpdateAsync(input);

        return CustomResponse(HttpStatusCode.OK, viewModel);
    }
    
    [HttpDelete("[action]")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        await service.DeleteAsync(id);
        return CustomResponse();
    }
}