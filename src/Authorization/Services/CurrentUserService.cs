using System;
using System.Threading.Tasks;
using Authorization.Entities;
using Authorization.Features.User;
using Authorization.Features.User.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Services
{
    public class CurrentUserService: ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<User> GetAsync()
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException();
            }
            var principal = _httpContextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<User> UpdateAsync(UpdateCurrentUserCommand command)
        {
            var user = await GetAsync();
            user.Firstname = command.Firstname;
            user.Lastname = command.Lastname;
            await _userManager.UpdateAsync(user);
            return user;
        }
    }
}