using System.Threading.Tasks;
using Authorization.Entities;
using Authorization.Features.User;
using Authorization.Features.User.Commands;

namespace Authorization.Services
{
    public interface ICurrentUserService
    {
        public Task<User> GetAsync();
        public Task<User> UpdateAsync(UpdateCurrentUserCommand command);
    }
}