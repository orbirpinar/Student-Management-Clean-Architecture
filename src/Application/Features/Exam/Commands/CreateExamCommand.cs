using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Exam.Commands;

public class CreateExamCommand : IRequest<Guid>
{
    public CreateExamCommand(Guid subjectId, Guid classRoomId)
    {
        SubjectId = subjectId;
        ClassRoomId = classRoomId;
    }

    public DateTime ExamDateTime { get; set; }
    public Guid SubjectId { get; }
    public Guid ClassRoomId { get; }
}

public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateExamCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateExamCommand request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects.FindAsync(request.SubjectId);
        if (subject == null)
        {
            throw new NotFoundException(nameof(subject), request.SubjectId.ToString());
        }

        var classRoom = await _context.ClassRoom.FindAsync(request.ClassRoomId);
        if (classRoom == null)
        {
            throw new NotFoundException(nameof(classRoom), request.ClassRoomId.ToString());
        }

        var exam = new Domain.Entities.Exam
        {
            ClassRoom = classRoom, Subject = subject, ExamDateTime = request.ExamDateTime
        };
        await _context.Exams.AddAsync(exam, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return exam.Id;
    }
}