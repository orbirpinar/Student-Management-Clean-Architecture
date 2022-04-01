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
    public class DeleteTeacherByAccountIdCommand: IRequest<Unit>
    {

        public Guid AccountId { get; set; }
    }
    
    public class DeleteTeacherCommandHandler: IRequestHandler<DeleteTeacherByAccountIdCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTeacherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeacherByAccountIdCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Where(t => t.Account!.Id == request.AccountId)
                .FirstOrDefaultAsync(cancellationToken);
            if (teacher is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.AccountId.ToString());
            }
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}