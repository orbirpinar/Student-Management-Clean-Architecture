using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Teacher: AuditableEntity
    {
        public Guid Id { get; set; }
        public Account Account { get; set; } = null!;
        
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        
        public ClassRoom? MainClassRoom { get; set; }
        
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}