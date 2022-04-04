using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Features.ClassRoom.Commands
{
    public class CreateClassRoomCommand: IRequest<Guid>
    {
        public byte Grade { get; set; }
        public string Group { get; set; }

    }
    
    public class CreateClassRoomCommandHandler: IRequestHandler<CreateClassRoomCommand,Guid>
    {

        private readonly IApplicationDbContext _context;

        public CreateClassRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateClassRoomCommand request, CancellationToken cancellationToken)
        {
            var classRoom = new Domain.Entities.ClassRoom {Grade = request.Grade, Group = request.Group};
            await _context.ClassRoom.AddAsync(classRoom,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return classRoom.Id;
        }
    }
}