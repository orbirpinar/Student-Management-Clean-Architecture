namespace Application.Interfaces;

public interface IUserSession
{
    string? Username { get; }
    bool IsAuthenticated { get; }
}