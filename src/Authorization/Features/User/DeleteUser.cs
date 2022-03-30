using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.User
{
    public class DeleteUser : IRequest<Unit>
    {
         public string Id { get; }

        public DeleteUser(string id)
        {
            Id = id;
        }
    }
    
    public class DeleteUserCommandHandler: IRequestHandler<DeleteUser,Unit>
    {
        private readonly UserManager<Entities.User> _userManager;

        public DeleteUserCommandHandler(UserManager<Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            await _userManager.DeleteAsync(user);
            return Unit.Value;
        }
    }
}