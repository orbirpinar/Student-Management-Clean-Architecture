using System;
using System.Threading.Tasks;
using Authorization.Common.Exceptions;
using Authorization.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shared.Events;

namespace Authorization.Consumer
{
    public class UserUpdatedConsumer: IConsumer<UserUpdated>
    {
        private readonly UserManager<User> _userManager;

        public UserUpdatedConsumer(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<UserUpdated> context)
        {
            var userId = context.Message.Id.ToString();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new EntityNotFoundException(nameof(user), userId);
            }

            user.Firstname = context.Message.Firstname;
            user.Lastname = context.Message.Lastname;
            await _userManager.UpdateAsync(user);
        }
    }
}