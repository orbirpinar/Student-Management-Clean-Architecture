using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.Student.Commands
{
    public class CreateStudentCommand : IRequest<Guid>
    {
        public CreateStudentCommand(string? firstName, string? lastName, Gender gender, DateTime birthDate, string? profilePicture)
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
        public DateTime BirthDate { get; }
        public string? ProfilePicture { get; }
    }


    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;


        public CreateStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student =  Domain.Entities.Student.Create(
                request.SchoolNumber, 
                request.FirstName, 
                request.LastName, 
                request.Gender,
                request.BirthDate, 
                request.ProfilePicture );

            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);
            return student.Id;
        }
    }
}