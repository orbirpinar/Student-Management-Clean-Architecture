﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.ClassRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ClassRoom");
                });

            modelBuilder.Entity("Domain.Entities.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassRommId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassRoomId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SubjectId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClassRoomId");

                    b.HasIndex("SubjectId1");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("ClassRommId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClassRoomId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<string>("SchoolNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClassRoomId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Entities.StudentScore", b =>
                {
                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("uuid");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.HasKey("StudentId", "ExamId");

                    b.HasIndex("ExamId");

                    b.ToTable("StudentScores");
                });

            modelBuilder.Entity("Domain.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Domain.Entities.TeacherSubject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("SubjectId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSubjects");
                });

            modelBuilder.Entity("Domain.Entities.Exam", b =>
                {
                    b.HasOne("Domain.Entities.ClassRoom", "ClassRoom")
                        .WithMany()
                        .HasForeignKey("ClassRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassRoom");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.HasOne("Domain.Entities.ClassRoom", "ClassRoom")
                        .WithMany("Students")
                        .HasForeignKey("ClassRoomId");

                    b.Navigation("ClassRoom");
                });

            modelBuilder.Entity("Domain.Entities.StudentScore", b =>
                {
                    b.HasOne("Domain.Entities.Exam", "Exam")
                        .WithMany("StudentScores")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Student", "Student")
                        .WithMany("StudentScores")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.OwnsOne("Domain.Entities.Account", "Account", b1 =>
                        {
                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Email");

                            b1.Property<string>("Firstname")
                                .HasColumnType("text")
                                .HasColumnName("Firstname");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("AccountId");

                            b1.Property<string>("Lastname")
                                .HasColumnType("text")
                                .HasColumnName("Lastname");

                            b1.Property<string>("Username")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Username");

                            b1.HasKey("TeacherId");

                            b1.ToTable("Teachers");

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.TeacherSubject", b =>
                {
                    b.HasOne("Domain.Entities.Subject", "Subject")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Teacher", "Teacher")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Domain.Entities.ClassRoom", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Domain.Entities.Exam", b =>
                {
                    b.Navigation("StudentScores");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.Navigation("StudentScores");
                });

            modelBuilder.Entity("Domain.Entities.Subject", b =>
                {
                    b.Navigation("TeacherSubjects");
                });

            modelBuilder.Entity("Domain.Entities.Teacher", b =>
                {
                    b.Navigation("TeacherSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
