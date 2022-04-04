using Application.Features.Student.Dtos;
using AutoMapper;

namespace Application.Features.Student.Maps
{
    public class StudentMappingProfile: Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Domain.Entities.Student, StudentWithClassRoomViewDto>();
            CreateMap<Domain.Entities.Student, StudentViewDto>();
        }
    }
}