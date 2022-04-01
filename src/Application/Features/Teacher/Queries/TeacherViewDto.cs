using System;
using System.Linq.Expressions;

namespace Application.Features.Teacher.Queries
{
    public class TeacherViewDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get;  set; } = default!;
        public string Email { get;  set; } = default!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        
        
    }
}