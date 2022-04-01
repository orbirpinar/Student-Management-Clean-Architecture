using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Subject: AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        
        public ICollection<Exam>? Exams { get; set; }
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}