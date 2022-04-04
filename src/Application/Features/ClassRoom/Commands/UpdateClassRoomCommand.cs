using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClassRoom.Commands
{
    public class UpdateClassRoomCommand: IRequest<Unit>
    {
        public Guid Id { get; set; }
        public byte Grade { get; set; }
        public string Group { get; set; } = default!;
    }
    
    public class UpdateClassRoomCommandHandler: IRequestHandler<UpdateClassRoomCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateClassRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateClassRoomCommand request, CancellationToken cancellationToken)
        {
            var classRoom = await _context.ClassRoom
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (classRoom is null)
            {
                throw new NotFoundException(nameof(classRoom), request.Id.ToString());
            }

            classRoom.Grade = request.Grade;
            classRoom.Group = request.Group;
            _context.ClassRoom.Update(classRoom);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}