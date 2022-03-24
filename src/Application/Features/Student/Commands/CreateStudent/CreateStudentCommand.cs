using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.Student.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<Guid>
    {
        public CreateStudentCommand(string? firstName, string? lastName, Gender gender, DateOnly birthDate, string? profilePicture)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            ProfilePicture = profilePicture;
        }

        public string SchoolNumber { get; } = null!;
        public string? FirstName { get; }
        public string? LastName { get; }
        public Gender Gender { get; }
        public DateOnly BirthDate { get; }
        public string? ProfilePicture { get; }
    }
    
    public class CreateStudentCommandHandler: IRequestHandler<CreateStudentCommand,Guid>
    {
        private readonly IApplicationDbContext _context;


        public CreateStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                SchoolNumber = request.SchoolNumber,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                ProfilePicture = request.ProfilePicture
            };
            _context.Students.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}