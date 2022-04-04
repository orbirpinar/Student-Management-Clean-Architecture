
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Student.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Queries
{
    public class GetAllStudentQuery: IRequest<List<StudentWithClassRoomViewDto>>
    {
    }
    
    public class GetAllStudentQueryHandler: IRequestHandler<GetAllStudentQuery,List<StudentWithClassRoomViewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStudentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StudentWithClassRoomViewDto>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.Students
                .Include(s => s.ClassRoom)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<StudentWithClassRoomViewDto>>(students);
        }
    }
}