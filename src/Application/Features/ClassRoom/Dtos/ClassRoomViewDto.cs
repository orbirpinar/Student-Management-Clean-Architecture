using System.Linq.Expressions;

namespace Application.Features.ClassRoom.Dtos
{
    public class ClassRoomViewDto
    {
        public Guid Id { get; set; }
        public byte Grade { get; set; }

        public string Group { get; set; } = default!;

        public static Expression<Func<Domain.Entities.ClassRoom, ClassRoomViewDto>> Projection
        {
            get
            {
                return x => new ClassRoomViewDto {Id = x.Id, Grade = x.Grade, Group = x.Group};
            }
        }
    }
}