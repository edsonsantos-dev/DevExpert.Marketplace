using System.Security.Claims;
using DevExpert.Marketplace.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Application.Helpers;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    public Guid GetUserId()
    {
        return !IsAuthenticated()
            ? Guid.Empty
            : Guid.Parse(accessor.HttpContext!.User.FindFirstValue("UserId") ?? string.Empty);
    }

    public bool IsAuthenticated()
    {
        return accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
    }
}