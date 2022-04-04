using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.Student.Commands
{
    public class CreateStudentCommand : IRequest<Guid>
    {

        public string SchoolNumber { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
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