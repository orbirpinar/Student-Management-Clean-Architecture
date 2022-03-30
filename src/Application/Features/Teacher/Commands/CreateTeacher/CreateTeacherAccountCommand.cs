using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Teacher.Commands.CreateTeacher
{
    public class CreateTeacherAccountCommand : IRequest<Guid>
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; } = default!;
        
        public string? Firstname { get; set; }
        
        public string? Lastname { get; set; }
        public string Email { get; set; } = default!;
    }


    public class CreateTeacherAccountCommandHandler : IRequestHandler<CreateTeacherAccountCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateTeacherAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTeacherAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.AccountId, request.Username, request.Email,request.Firstname,request.Lastname);
            var teacher = new Domain.Entities.Teacher { Account = account};
            await _context.Teachers.AddAsync(teacher, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return teacher.Id;
        }
    }
}