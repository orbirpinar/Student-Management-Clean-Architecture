using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Events;

namespace Authorization.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
         public string Id { get; }

        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }
    
    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand,Unit>
    {
        private readonly UserManager<Entities.User> _userManager;
        private readonly IBus _bus;

        public DeleteUserCommandHandler(UserManager<Entities.User> userManager, IBus bus)
        {
            _userManager = userManager;
            _bus = bus;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            await _userManager.DeleteAsync(user);
            await _bus.Publish<UserDeleted>(new {request.Id}, cancellationToken);
            return Unit.Value;
        }
    }
}