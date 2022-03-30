using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Teacher> Teachers { get; }
        public DbSet<Student> Students { get; }
        public DbSet<Subject> Subjects { get; }
        public DbSet<ClassRoom> ClassRoom { get; }
        public DbSet<Exam> Exams { get; }
        public DbSet<StudentScore> StudentScores { get; }
        public DbSet<TeacherSubject> TeacherSubjects { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}