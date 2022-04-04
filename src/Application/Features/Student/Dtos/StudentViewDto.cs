using System;
using Domain.Enums;

namespace Application.Features.Student.Dtos
{
    public class StudentViewDto
    {
        
        public Guid Id { get; set; }
        public string? SchoolNumber { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
    }
}