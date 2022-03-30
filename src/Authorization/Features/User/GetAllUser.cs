using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authorization.ViewModels;
using MassTransit.Futures.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Features.User
{
    public class GetAllUsers: IRequest<List<UserViewModel>>
    {

    }
    
    public class GetAllUsersHandler: IRequestHandler<GetAllUsers,List<UserViewModel>>
    {
        private readonly UserManager<Entities.User> _userManager;

        public GetAllUsersHandler(UserManager<Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.Select(UserViewModel.Projection).ToListAsync(cancellationToken);
        }
    }
}