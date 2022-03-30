using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.Role.Commands
{
    public class DeleteRoleByName: IRequest<Unit>
    {
        public DeleteRoleByName(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
    
    public class DeleteRoleByNameCommandHandler: IRequestHandler<DeleteRoleByName,Unit>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteRoleByNameCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(DeleteRoleByName request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);
            await _roleManager.DeleteAsync(role);
            return Unit.Value;
        }
    }
}