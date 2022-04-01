using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Exam: AuditableEntity
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; } 
        public Guid SubjectId { get; set; }
        public ClassRoom ClassRoom { get; set; } = null!;
        public Guid ClassRommId { get; set; }
        public ICollection<StudentScore>? StudentScores { get; set; }
    }
}