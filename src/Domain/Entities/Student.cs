using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Student : AuditableEntity
    {
        public Guid Id { get; private set; }
        public string? SchoolNumber { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string? ProfilePicture { get; private set; }
        public Guid? ClassRoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }
        public ICollection<StudentScore>? StudentScores { get; set; }

        private Student(string? schoolNumber, string? firstName, string? lastName, Gender gender, DateTime? birthDate,
            string? profilePicture)
        {
            SchoolNumber = schoolNumber;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            ProfilePicture = profilePicture;
        }

        public static Student Create(string? schoolNumber, string? firstName, string? lastName, Gender gender,
            DateTime? birthDate, string? profilePicture)
        {
            return new Student(
                schoolNumber,
                firstName,
                lastName,
                gender,
                birthDate,
                profilePicture
            );
        }

        public void Update(string? schoolNumber, string? firstName, string? lastName, Gender gender,
                                                 DateTime? birthDate, string? profilePicture)
        {
            SchoolNumber = schoolNumber;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            ProfilePicture = profilePicture;
        }
    }
}