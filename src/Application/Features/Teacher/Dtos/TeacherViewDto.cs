using System;
using Application.Features.ClassRoom.Dtos;

namespace Application.Features.Teacher.Dtos
{
    public class TeacherViewDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get;  set; } = default!;
        public string Email { get;  set; } = default!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        
        public ClassRoomViewDto? MainClassRoom { get; set; }
        
        
    }
}