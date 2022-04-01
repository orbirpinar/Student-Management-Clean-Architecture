using Application.Features.Teacher.Queries;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public static class Extensions
    {
        public static TeacherViewDto ToViewDto(this Teacher teacher)
        {
            return new TeacherViewDto
            {
                Id = teacher.Id,
                AccountId = teacher.Account.Id,
                Username = teacher.Account.Username,
                Email = teacher.Account.Email,
                Firstname = teacher.Account.Firstname,
                Lastname = teacher.Account.Lastname,
                BirthDate = teacher.BirthDate,
                Phone = teacher.Phone,
                ProfilePicture = teacher.ProfilePicture
            };
        }
    }
}