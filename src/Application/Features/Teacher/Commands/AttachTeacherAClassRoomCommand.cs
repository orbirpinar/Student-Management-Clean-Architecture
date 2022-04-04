using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Teacher.Commands
{
    public class AttachTeacherAClassRoomCommand: IRequest<Unit>
    {
        public AttachTeacherAClassRoomCommand(Guid classRoomId, Guid teacherId)
        {
            ClassRoomId = classRoomId;
            TeacherId = teacherId;
        }

        public Guid TeacherId { get; set; }
        public Guid ClassRoomId { get; }
    }
    
    public class AttachTeacherAClassRoomCommandHandler: IRequestHandler<AttachTeacherAClassRoomCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public AttachTeacherAClassRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AttachTeacherAClassRoomCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Where(t => t.Id == request.TeacherId)
                .FirstOrDefaultAsync(cancellationToken);
            if (teacher is null)
            {
                throw new NotFoundException(nameof(teacher), request.TeacherId.ToString());
            }

            var mainClassRoom = await _context.ClassRoom.Where(c => c.Id == request.ClassRoomId)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (mainClassRoom is null)
            {
                throw new NotFoundException(nameof(mainClassRoom), request.ClassRoomId.ToString());
            }

            teacher.MainClassRoom = mainClassRoom;
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}