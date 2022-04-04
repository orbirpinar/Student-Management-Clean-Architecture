using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;


        public AppDbContext(
            DbContextOptions<AppDbContext> options, IDateTime dateTime
        )
            : base(options)
        {
            _dateTime = dateTime;
        }



        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<ClassRoom> ClassRoom => Set<ClassRoom>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<StudentScore> StudentScores => Set<StudentScore>();
        public DbSet<TeacherSubject> TeacherSubjects => Set<TeacherSubject>();


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<StudentScore>()
                .HasKey(ss => new {ss.StudentId, ss.ExamId});
            builder.Entity<StudentScore>()
                .HasOne(ss => ss.Exam)
                .WithMany(s => s.StudentScores)
                .HasForeignKey(ss => ss.ExamId);
            builder.Entity<StudentScore>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentScores)
                .HasForeignKey(ss => ss.StudentId);

            builder.Entity<TeacherSubject>()
                .HasKey(ts => new {ts.SubjectId, ts.TeacherId});
            builder.Entity<TeacherSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.TeacherSubjects)
                .HasForeignKey(ss => ss.SubjectId);
            builder.Entity<TeacherSubject>()
                .HasOne(ss => ss.Teacher)
                .WithMany(s => s.TeacherSubjects)
                .HasForeignKey(ss => ss.TeacherId);

            builder.Entity<ClassRoom>().HasOne(c => c.MainTeacher)
                .WithOne(t => t.MainClassRoom)
                .HasForeignKey<ClassRoom>(c => c.MainTeacherId);




            //Value Object
            builder.Entity<Teacher>().OwnsOne(t => t.Account, navigationBuilder =>
            {
                navigationBuilder.Property(account => account.Id).HasColumnName("AccountId");
                navigationBuilder.Property(account => account.Lastname).HasColumnName("Lastname");
                navigationBuilder.Property(account => account.Email).HasColumnName("Email");
                navigationBuilder.Property(account => account.Username).HasColumnName("Username");
                navigationBuilder.Property(account => account.Firstname).HasColumnName("Firstname");
                navigationBuilder.Property(account => account.Lastname).HasColumnName("Lastname");
                navigationBuilder.HasIndex(a => a.Id).IsUnique();
                navigationBuilder.HasIndex(a => a.Email).IsUnique();
                navigationBuilder.HasIndex(a => a.Firstname).IsUnique();
            });
        }
    }
}