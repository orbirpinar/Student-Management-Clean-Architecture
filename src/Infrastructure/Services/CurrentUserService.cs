using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CurrentUserService: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IUserSession GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return new UserSession();
        }

        var currentUser = new UserSession
        {
            IsAuthenticated = _httpContextAccessor.HttpContext.User.Identity is {IsAuthenticated: true},
            Username = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value
        };
        return currentUser;
    }
}