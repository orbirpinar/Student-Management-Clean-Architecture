using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class ClassRoom : AuditableEntity
    {
        public Guid Id { get; set; }
        public byte Grade { get; set; }

        public string Group { get; set; } = default!;
        
        public Teacher? MainTeacher { get; set; }
        public Guid? MainTeacherId { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}