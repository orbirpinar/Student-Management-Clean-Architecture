using System;

namespace Domain.Entities
{
    public class StudentScore
    {
        public Student Student { get; set; } = null!;
        public Guid? StudentId { get; set; } = null!;

        public Exam Exam { get; set; } = null!;
        public Guid ExamId { get; set; }
        
        public double Score { get; set; }
    }
}