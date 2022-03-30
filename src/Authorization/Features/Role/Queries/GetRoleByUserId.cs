using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authorization.Common.Exceptions;
using Authorization.ViewModels;
using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Features.Role.Queries
{
    public class GetRoleByUserId: IRequest<ListOfRoleViewModel>
    {
        public string UserId;

        public GetRoleByUserId(string userId)
        {
            UserId = userId;
        }
    }
    
    public class  GetRoleByUserIdHandler: IRequestHandler<GetRoleByUserId,ListOfRoleViewModel>
    {
        private readonly UserManager<Entities.User> _userManager;


        public GetRoleByUserIdHandler(UserManager<Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ListOfRoleViewModel> Handle(GetRoleByUserId request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                throw new EntityNotFoundException(nameof(user), request.UserId);
            }
            return await _userManager.GetRolesAsync(user).Select(r => new ListOfRoleViewModel {roleNameList = r});
        }
    }
}