using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Student: AuditableEntity
    {
        public Guid Id { get; set; }
        public string? SchoolNumber { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
        public Guid? ClassRommId { get; set; }
        public ClassRoom? ClassRoom { get; set; }
        public ICollection<StudentScore>? StudentScores { get; set; }
    }
}