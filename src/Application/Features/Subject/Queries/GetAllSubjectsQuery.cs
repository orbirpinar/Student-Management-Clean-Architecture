using Application.Features.Subject.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subject.Queries;

public class GetAllSubjectsQuery: IRequest<List<SubjectViewDto>>
{
    
}

public class GetAllSubjectQueryHandler : IRequestHandler<GetAllSubjectsQuery, List<SubjectViewDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllSubjectQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubjectViewDto>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = await _context.Subjects.ToListAsync(cancellationToken);
        return subjects.Select(s => new SubjectViewDto(s.Id, s.Name)).ToList();
    }
}