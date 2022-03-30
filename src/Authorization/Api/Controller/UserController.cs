using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization.Api.Request;
using Authorization.Features.Role.Queries;
using Authorization.Features.User;
using Authorization.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Authorization.Api.Controller
{
    [ApiController]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task UpdateUser(string id,[FromBody] UpdateUser user)
        {
            user.Id = id;
            await _mediator.Send(user);
        }

        [HttpDelete("{id}")]
        public async Task DeleteUser(string id)
        {
            await _mediator.Send(new DeleteUser(id));
        }

        [HttpPut("{id}/roles")]
        public async Task AssignRole(string id,AttachRoleToUserRequest request)
        {
            var command = new AttachRoleToUser {RoleName = request.RoleName,UserId = id};
            await _mediator.Send(command);
        }
        
        [HttpGet("{userId}/roles")]
        public async Task<ListOfRoleViewModel> GetUserRoles(string userId)
        {
            return await _mediator.Send(new GetRoleByUserId(userId));
        }

        [HttpGet]
        public async Task<List<UserViewModel>> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUsers());
        }
    }
}