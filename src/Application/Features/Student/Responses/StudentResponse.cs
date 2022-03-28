using System;
using System.Linq.Expressions;
using Domain.Enums;

namespace Application.Features.Student.Responses
{
    public class StudentResponse
    {
        
        public Guid Id { get; set; }
        public string? SchoolNumber { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? ProfilePicture { get; set; }

        public static Expression<Func<Domain.Entities.Student, StudentResponse>> Projection
        {
            get
            {
                return x => new StudentResponse()
                {
                    Id = x.Id,
                    SchoolNumber = x.SchoolNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    BirthDate = x.BirthDate,
                    ProfilePicture = x.ProfilePicture
                };
            }
        }
    }
}