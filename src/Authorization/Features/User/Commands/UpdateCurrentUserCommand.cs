using System.Threading;
using System.Threading.Tasks;
using Authorization.Services;
using MassTransit;
using MediatR;
using Shared.Events;

namespace Authorization.Features.User.Commands
{
    public class UpdateCurrentUserCommand: IRequest<Unit>
    {
        public string? Firstname { get; }
        public string? Lastname { get; }

        public UpdateCurrentUserCommand(string? lastname, string? firstname)
        {
            Lastname = lastname;
            Firstname = firstname;
        }
    }
    
    
    public class UpdateCurrentUserCommandHandler: IRequestHandler<UpdateCurrentUserCommand,Unit>
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly IBus _bus;
        

        public UpdateCurrentUserCommandHandler(ICurrentUserService currentUserService, IBus bus)
        {
            _currentUserService = currentUserService;
            _bus = bus;
        }

        public async Task<Unit> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _currentUserService.UpdateAsync(request);
            await _bus.Publish<UserUpdated>(new {user.Id,user.Firstname, user.Lastname}, cancellationToken);
            return Unit.Value;
        }
    }
}