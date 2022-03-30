using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.Role.Commands
{
    public class CreateRole: IRequest<string>
    {
        public CreateRole(string name)
        {
            Name = name;
        }

        public string Name { get;}
    }
    
    public class CreateRoleCommandHandler: IRequestHandler<CreateRole,string>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var role = new IdentityRole {Name = request.Name};
            
            await _roleManager.CreateAsync(role);
            return role.Id;
        }
    }
}