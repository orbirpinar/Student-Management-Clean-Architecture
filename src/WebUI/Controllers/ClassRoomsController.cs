using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.ClassRoom.Commands;
using Application.Features.ClassRoom.Dtos;
using Application.Features.ClassRoom.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/class-rooms")]
    public class ClassRoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassRoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> Create(CreateClassRoomCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id:guid}")]
        public async Task Update(Guid id, UpdateClassRoomCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await _mediator.Send(new DeleteClassRoomCommand(id));
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ClassRoomViewDto> GetById(Guid id)
        {
            return await _mediator.Send(new GetClassRoomByIdQuery(id));
        }

        [HttpGet]
        public async Task<List<ClassRoomViewDto>> GetAll()
        {
            return await _mediator.Send(new GetAllClassRoomsQuery());
        }

 
    }
}