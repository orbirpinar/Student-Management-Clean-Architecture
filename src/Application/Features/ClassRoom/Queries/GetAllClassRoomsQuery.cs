using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.ClassRoom.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClassRoom.Queries
{
    public class GetAllClassRoomsQuery: IRequest<List<ClassRoomViewDto>>
    {
    }
    
    public class GetAllClassRoomQueryHandler: IRequestHandler<GetAllClassRoomsQuery,List<ClassRoomViewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllClassRoomQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClassRoomViewDto>> Handle(GetAllClassRoomsQuery request, CancellationToken cancellationToken)
        {
            var classRoom = await _context.ClassRoom
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<ClassRoomViewDto>>(classRoom);
        }
    }
}