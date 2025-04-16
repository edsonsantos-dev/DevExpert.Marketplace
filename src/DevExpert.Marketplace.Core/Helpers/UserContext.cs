using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Core.Helpers;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    public Guid GetUserId()
    {
        if (!IsAuthenticated())
            return Guid.Empty;

        var userIdClaim = accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? accessor.HttpContext!.User.FindFirstValue("UserId"); // Para JWTs (sub)

        return Guid.TryParse(userIdClaim, out var userId) 
            ? userId 
            : Guid.Empty;
    }

    public bool IsAuthenticated()
    {
        return accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
    }
}