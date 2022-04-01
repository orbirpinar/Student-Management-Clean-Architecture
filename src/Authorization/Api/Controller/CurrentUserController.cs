using System.Threading.Tasks;
using Authorization.Features.User;
using Authorization.Features.User.Commands;
using Authorization.Features.User.Queries;
using Authorization.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controller
{
    [ApiController]
    [Route("api/users/me")]
    [Authorize]
    public class CurrentUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrentUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task Update(UpdateCurrentUserCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<UserViewModel> Get()
        {
            return await _mediator.Send(new GetCurrentUserQuery());
        }
    }
}