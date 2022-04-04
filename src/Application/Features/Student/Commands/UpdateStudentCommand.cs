using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Commands
{
    public class UpdateStudentCommand : IRequest
    {
        public UpdateStudentCommand(Guid id, string? schoolNumber, string? firstName, string? lastName, Gender gender, DateTime birthDate, string? profilePicture)
        {
            Id = id;
            SchoolNumber = schoolNumber;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            ProfilePicture = profilePicture;
        }

        public Guid Id { get; }
        public string? SchoolNumber { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
        public Gender Gender { get; }
        public DateTime BirthDate { get; }
        public string? ProfilePicture { get; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Students.FirstAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.Id.ToString());
            }

            entity.SchoolNumber = request.SchoolNumber;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Gender = request.Gender;
            entity.BirthDate = request.BirthDate;
            entity.ProfilePicture = request.ProfilePicture;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}