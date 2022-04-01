using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Events;

namespace Application.Features.Teacher.Commands
{
    public class UpdateTeacherCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public UpdateTeacherCommand(Guid id)
        {
            Id = id;
        }

        public DateOnly? BirthDate { get; set; }

        public string? ProfilePicture { get; set; }

        public string? Phone { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }

    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IBus _bus;

        public UpdateTeacherCommandHandler(IApplicationDbContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Where(t => t.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (teacher is null)
            {
                throw new NotFoundException(nameof(teacher), request.Id.ToString());
            }

            teacher.BirthDate = request.BirthDate;
            teacher.Phone = request.Phone;
            teacher.ProfilePicture = request.ProfilePicture;
            if (request.Firstname is not null || request.Lastname is not null)
            {
                teacher.Account.Firstname = request.Firstname;
                teacher.Account.Lastname = request.Lastname;
                await _bus.Publish<UserUpdated>(new {teacher.Account.Id, request.Firstname, request.Lastname}, cancellationToken);
            }


            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}