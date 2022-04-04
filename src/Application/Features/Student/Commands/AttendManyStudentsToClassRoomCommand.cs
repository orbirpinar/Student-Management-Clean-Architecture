using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Commands
{
    public class AttendManyStudentsToClassRoomCommand : IRequest<Unit>
    {
        public AttendManyStudentsToClassRoomCommand(List<Guid> studentIdList,Guid classRoomId)
        {
            StudentIdList = studentIdList;
            ClassRoomId = classRoomId;
        }
        public List<Guid> StudentIdList { get; }
        public Guid ClassRoomId { get; }
    }

    public class AttendManyStudentsToClassRoomHandler : IRequestHandler<AttendManyStudentsToClassRoomCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public AttendManyStudentsToClassRoomHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AttendManyStudentsToClassRoomCommand request, CancellationToken cancellationToken)
        {
            var classRoom = await _context.ClassRoom.Where(c => c.Id == request.ClassRoomId)
                .FirstOrDefaultAsync(cancellationToken);
            if (classRoom is null)
            {
                throw new NotFoundException(nameof(classRoom), request.ClassRoomId.ToString());
            }
            foreach (Guid studentId in request.StudentIdList)
            {
                var student = await _context.Students.Where(student => student.Id == studentId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (student is null)
                {
                    continue;
                }
                student.ClassRoom = classRoom;
                _context.Students.Update(student);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}