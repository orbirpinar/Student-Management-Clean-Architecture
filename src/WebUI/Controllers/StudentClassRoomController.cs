using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Student.Dtos;
using Application.Features.Student.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("/api")]
    public class StudentClassRoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentClassRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("class-rooms/{classRoomId:guid}/students")]
        public async Task<List<StudentViewDto>> GetStudentsByClassRoomId(Guid classRoomId)
        {
            return await _mediator.Send(new GetStudentsByClassRoomId(classRoomId));
        }
    }
}