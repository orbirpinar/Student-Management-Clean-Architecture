using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;


        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, 
            ICurrentUserService currentUserService, IDateTime dateTime,
             IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options,operationalStoreOptions)
        {
            _currentUserService = currentUserService;
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
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
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
        }
    }
}