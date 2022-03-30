using System.Threading;
using System.Threading.Tasks;
using Authorization.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.User
{
    public class AttachRoleToUser: IRequest<Unit>
    {
        public string UserId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
    
    public class AttachRoleToUserCommandHandler: IRequestHandler<AttachRoleToUser,Unit>
    {
        private readonly UserManager<Entities.User> _userManager;

        public AttachRoleToUserCommandHandler(UserManager<Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(AttachRoleToUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                throw new EntityNotFoundException(nameof(user), request.UserId);
            }
            await _userManager.AddToRoleAsync(user, request.RoleName);
            return Unit.Value;
        }
    }
}