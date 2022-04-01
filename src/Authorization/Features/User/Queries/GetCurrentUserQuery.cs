using System.Threading;
using System.Threading.Tasks;
using Authorization.Services;
using Authorization.ViewModels;
using MassTransit.Initializers;
using MediatR;

namespace Authorization.Features.User.Queries
{
    public class GetCurrentUserQuery: IRequest<UserViewModel>
    {
    }
    
    
    public class GetCurrentUserQueryHandler: IRequestHandler<GetCurrentUserQuery,UserViewModel>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<UserViewModel> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _currentUserService.GetAsync();
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname
            };
        }
    }
}