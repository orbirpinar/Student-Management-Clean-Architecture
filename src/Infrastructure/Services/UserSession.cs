using Application.Interfaces;

namespace Infrastructure.Services;

public class UserSession: IUserSession
{
    public string? Username { get; set; }
    public bool IsAuthenticated { get; set; }
}