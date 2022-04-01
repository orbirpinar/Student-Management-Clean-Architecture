using System.Threading;
using System.Threading.Tasks;
using Authorization.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.User.Commands
{
    public class UpdateUserCommand: IRequest<Unit>
    {
        public UpdateUserCommand(string id, string? email, string? username, string? firstname, string? lastname)
        {
            Id = id;
            Email = email;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Id { get; set; }
        public string? Username { get; }
        public string? Email { get;}
        public string? Firstname { get;}
        public string? Lastname { get; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<Entities.User> _userManager;

        public UpdateUserCommandHandler(UserManager<Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                throw new EntityNotFoundException(nameof(user), request.Id);
            }
            user.UserName = request.Username;
            user.Email = request.Email;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}