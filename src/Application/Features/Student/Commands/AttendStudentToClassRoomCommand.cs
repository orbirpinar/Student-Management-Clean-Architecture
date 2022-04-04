using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Commands
{
    public class AttendStudentToClassRoomCommand: IRequest<Unit>
    {
        public AttendStudentToClassRoomCommand(Guid studentId, Guid classRoomId)
        {
            StudentId = studentId;
            ClassRoomId = classRoomId;
        }

        public Guid StudentId { get; set; }

        public Guid ClassRoomId { get; }
    }
    
    public class AttendStudentClassRoomCommandHandler: IRequestHandler<AttendStudentToClassRoomCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public AttendStudentClassRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AttendStudentToClassRoomCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.Where(s => s.Id == request.StudentId)
                .FirstOrDefaultAsync(cancellationToken);
            if (student is null)
            {
                throw new NotFoundException(nameof(student), request.StudentId.ToString());
            }

            var classRoom = await _context.ClassRoom.Where(c => c.Id == request.ClassRoomId)
                .FirstOrDefaultAsync(cancellationToken);

            if (classRoom is null)
            {
                throw new NotFoundException(nameof(classRoom), request.ClassRoomId.ToString());
            }

            student.ClassRoom = classRoom;
            _context.Students.Update(student);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}