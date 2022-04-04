using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.ClassRoom.Dtos;
using Application.Interfaces;
using AutoMapper;
using MassTransit.Futures.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClassRoom.Queries
{
    public class GetClassRoomByIdQuery: IRequest<ClassRoomViewDto>
    {
        public GetClassRoomByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
    public class GetClassRoomByIdQueryHandler: IRequestHandler<GetClassRoomByIdQuery,ClassRoomViewDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClassRoomByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClassRoomViewDto> Handle(GetClassRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var classRoom = await _context.ClassRoom.Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (classRoom is null)
            {
                throw new NotFoundException(nameof(classRoom), request.Id.ToString());
            }

            return _mapper.Map<ClassRoomViewDto>(classRoom);

        }
    }
}