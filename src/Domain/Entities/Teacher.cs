using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Teacher: AuditableEntity
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateOnly? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}