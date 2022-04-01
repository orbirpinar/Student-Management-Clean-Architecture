using System;
using System.Linq.Expressions;
using Domain.Entities;

namespace WebUI.Responses
{
    public class TeacherViewModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        
        
        public static Expression<Func<Teacher,TeacherViewModel>> MapFromEntity
        {
            get
            {
                return x => new TeacherViewModel
                {
                    Id = x.Id,
                    AccountId = x.Account.Id, 
                    Username = x.Account.Username,
                    Email = x.Account.Email,
                    Firstname = x.Account.Firstname,
                    Lastname = x.Account.Lastname,
                    BirthDate = x.BirthDate,
                    Phone = x.Phone,
                    ProfilePicture = x.ProfilePicture
                };
            }
        }
    }
}