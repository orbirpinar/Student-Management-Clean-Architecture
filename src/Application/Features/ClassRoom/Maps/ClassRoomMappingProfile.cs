using Application.Features.ClassRoom.Dtos;
using AutoMapper;

namespace Application.Features.ClassRoom.Maps
{
    public class ClassRoomMappingProfile: Profile
    {
        public ClassRoomMappingProfile()
        {
            CreateMap<Domain.Entities.ClassRoom, ClassRoomViewDto>();
        }
    }
}