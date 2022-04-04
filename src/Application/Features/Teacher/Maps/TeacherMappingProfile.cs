using Application.Features.Teacher.Dtos;
using AutoMapper;

namespace Application.Features.Teacher.Maps
{
    public class TeacherMappingProfile: Profile
    {
        public TeacherMappingProfile()
        {
            CreateMap<Domain.Entities.Teacher, TeacherViewDto>();
        }
    }
}