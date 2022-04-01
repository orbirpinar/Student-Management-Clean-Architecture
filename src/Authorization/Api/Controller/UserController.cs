using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization.Api.Request;
using Authorization.Features.Role.Queries;
using Authorization.Features.User;
using Authorization.Features.User.Commands;
using Authorization.Features.User.Queries;
using Authorization.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Authorization.Api.Controller
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "ADMIN",AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task Update(string id,[FromBody] UpdateUserCommand userCommand)
        {
            userCommand.Id = id;
            await _mediator.Send(userCommand);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
        }

        [HttpPut("{id}/roles")]
        public async Task AssignRole(string id,AttachRoleToUserRequest request)
        {
            var command = new AttachRoleToUserCommand {RoleName = request.RoleName,UserId = id};
            await _mediator.Send(command);
        }
        
        [HttpGet("{userId}/roles")]
        public async Task<ListOfRoleViewModel> GetUserRoles(string userId)
        {
            return await _mediator.Send(new GetRoleByUserId(userId));
        }

        [HttpGet]
        public async Task<List<UserViewModel>> GetAll()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }
    }
}