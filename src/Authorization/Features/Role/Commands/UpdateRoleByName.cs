using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.Role.Commands
{
    public class UpdateRoleByName: IRequest<Unit>
    {
        public UpdateRoleByName(string existingRoleName, string newRoleName)
        {
            ExistingRoleName = existingRoleName;
            this.newRoleName = newRoleName;
        }

        public string ExistingRoleName { get;}
        public string newRoleName { get; }
    }
    
    public class UpdateRoleByNameCommandHandler: IRequestHandler<UpdateRoleByName,Unit>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRoleByNameCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(UpdateRoleByName request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.ExistingRoleName);
            role.Name = request.newRoleName;
            await _roleManager.UpdateAsync(role);
            return Unit.Value;
        }
    }
}