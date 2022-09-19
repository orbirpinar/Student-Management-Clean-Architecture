using Application.Features.Subject.Dtos;
using Application.Features.Subject.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectController
{

    private readonly IMediator _mediator;

    public SubjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<SubjectViewDto>> GetAll()
    {
        return await _mediator.Send(new GetAllSubjectsQuery());
    }
}