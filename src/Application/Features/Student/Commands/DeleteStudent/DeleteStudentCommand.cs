using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Student.Commands.DeleteStudent
{
    public class DeleteStudentCommand: IRequest
    {
        public DeleteStudentCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
    public class DeleteStudentCommandHandler: IRequestHandler<DeleteStudentCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Students.FindAsync(new object?[] { request.Id}, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.Id.ToString());
            }

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}