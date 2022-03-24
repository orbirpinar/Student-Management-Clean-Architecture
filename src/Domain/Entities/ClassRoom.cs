using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class ClassRoom : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<Student>? Students { get; set; }
    }
}