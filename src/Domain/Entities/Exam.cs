using Domain.Common;

namespace Domain.Entities
{
    public class Exam: AuditableEntity
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; } = null!;
        public ClassRoom ClassRoom { get; set; } = null!;
        public DateTime ExamDateTime { get; set; }
        public ICollection<StudentScore>? StudentScores { get; set; }
    }
}