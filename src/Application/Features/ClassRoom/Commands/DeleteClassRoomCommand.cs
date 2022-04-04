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
    public class DeleteClassRoomCommand: IRequest<Unit>
    {
        public DeleteClassRoomCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
    public class DeleteClassRoomCommandHandler: IRequestHandler<DeleteClassRoomCommand,Unit>
    {

        private readonly IApplicationDbContext _context;

        public DeleteClassRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteClassRoomCommand request, CancellationToken cancellationToken)
        {
            var classRoom = await _context.ClassRoom
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (classRoom is null)
            {
                throw new NotFoundException(nameof(classRoom), request.Id.ToString());
            }

            _context.ClassRoom.Remove(classRoom);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}