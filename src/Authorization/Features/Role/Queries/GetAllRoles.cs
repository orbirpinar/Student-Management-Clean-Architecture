using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authorization.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Features.Role.Queries
{
    public class GetAllRoles: IRequest<List<RoleViewModel>>
    {
    }
    
    public class GetAllRolesHandler: IRequestHandler<GetAllRoles,List<RoleViewModel>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllRolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleViewModel>> Handle(GetAllRoles request, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.Select(RoleViewModel.Projection).ToListAsync(cancellationToken);
        }
    }
}