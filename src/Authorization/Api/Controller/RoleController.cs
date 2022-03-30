using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization.Features.Role.Commands;
using Authorization.Features.Role.Queries;
using Authorization.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Authorization.Api.Controller
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class RoleController: ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<string> Create(CreateRole command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task Update(UpdateRoleByName command)
        {
            await _mediator.Send(command);
        }
        
        [HttpGet]
        public async Task<List<RoleViewModel>> GetAll()
        {
            return await _mediator.Send(new GetAllRoles());
        }

        [HttpGet("{name}")]
        public async Task<RoleViewModel> GetByName(string name)
        {
            return await _mediator.Send(new GetRoleByName(name));
        }

    }
}