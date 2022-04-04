using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Student.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Queries
{
    public class GetStudentsByClassRoomId: IRequest<List<StudentViewDto>>
    {
        public GetStudentsByClassRoomId(Guid classRoomId)
        {
            ClassRoomId = classRoomId;
        }

        public Guid ClassRoomId { get; }
    }
    
    public class GetStudentsByClassRoomIdHandler: IRequestHandler<GetStudentsByClassRoomId,List<StudentViewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentsByClassRoomIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StudentViewDto>> Handle(GetStudentsByClassRoomId request, CancellationToken cancellationToken)
        {
            var students = await _context.Students.Where(x => x.ClassRoomId == request.ClassRoomId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<StudentViewDto>>(students);
        }
    }
}