using Application.Features.Exam.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("api/exams")]
public class ExamController
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<Guid> Create(CreateExamCommand command)
    {
        return await _mediator.Send(command);
    }
}