namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        IUserSession GetCurrentUser();
    }
}