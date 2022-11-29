using Domain.Common;

namespace Domain.Entities
{
    public class Exam: AuditableEntity
    {
        private Exam(Subject subject, ClassRoom classRoom, DateTime examDateTime)
        {
            Subject = subject;
            ClassRoom = classRoom;
            ExamDateTime = examDateTime;
        }


        public Guid Id { get; set; }
        public Subject Subject { get; private set; } = null!;
        public ClassRoom ClassRoom { get; private set; } = null!;
        public DateTime ExamDateTime { get; private set; }
        public ICollection<StudentScore>? StudentScores { get; set; }


        public static Exam Create(Subject subject, ClassRoom classRoom, DateTime examDateTime)
        {
            return new Exam(subject, classRoom, examDateTime);
        }
    }
}