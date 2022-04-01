using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Teacher.Commands;
using Application.Features.Teacher.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Responses;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id:guid}")]
        public async Task Update(Guid id, UpdateTeacherCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<TeacherViewDto>> GetAll()
        {
            return await _mediator.Send(new GetAllTeachersQuery());
        }
    }
}