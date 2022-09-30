using Authorization.Entities;
using Authorization.Features.User.Commands;

namespace Authorization.Services;

public interface ICurrentUserService
{
    public Task<User> GetAsync();
    public Task<User> UpdateAsync(UpdateCurrentUserCommand command);
}