using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authorization.Common.Exceptions;
using Authorization.ViewModels;
using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Features.Role.Queries
{
    public class GetRoleByName: IRequest<RoleViewModel>
    {
        public string Name;

        public GetRoleByName(string name)
        {
            Name = name;
        }
    }
    
    public class GetRoleByNameHandler: IRequestHandler<GetRoleByName,RoleViewModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRoleByNameHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<RoleViewModel> Handle(GetRoleByName request, CancellationToken cancellationToken)
        {
            var role =  await _roleManager.Roles
                .Select(RoleViewModel.Projection)
                .Where(r => r.Name == request.Name)
                .FirstOrDefaultAsync(cancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(nameof(role), request.Name);
            }

            return role;
        }
    }
}