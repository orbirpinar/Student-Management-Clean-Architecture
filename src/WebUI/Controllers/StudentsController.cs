using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Student.Commands;
using Application.Features.Student.Dtos;
using Application.Features.Student.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    
    
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<StudentWithClassRoomViewDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllStudentQuery());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<StudentWithClassRoomViewDto>> GetById(Guid id)
        {
            return await _mediator.Send(new GetStudentByIdQuery(id));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateStudentCommand studentCommand)
        {
            return await _mediator.Send(studentCommand);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update([FromBody] UpdateStudentCommand command, [FromRoute] Guid id)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteStudentCommand(id));
            return NoContent();
        }

        [HttpPost("{id:guid}/class-rooms")]
        public async Task<IActionResult> AttendToClassRoom(Guid id, AttendStudentToClassRoomCommand command)
        {
            command.StudentId = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("multiple/class-rooms")]
        public async Task<IActionResult> AttendManyStudentsToClassRoom(AttendManyStudentsToClassRoomCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        
        
    }
}