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
    public class UpdateTeacherAccountCommand: IRequest<Unit>
    {
        public Guid? AccountId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
    
    public class UpdateTeacherAccountCommandHandler: IRequestHandler<UpdateTeacherAccountCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTeacherAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context; }

        public async Task<Unit> Handle(UpdateTeacherAccountCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Where(t => t.Account!.Id == request.AccountId)
                .FirstOrDefaultAsync(cancellationToken);
            if (teacher?.Account is null)
            {
                throw new NotFoundException(nameof(teacher), request.AccountId.ToString()!);
            }
            teacher.Account.Firstname = request.Firstname;
            teacher.Account.Lastname = request.Lastname;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}