using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Teacher.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Teacher.Queries
{
    public class GetAllTeachersQuery: IRequest<List<TeacherViewDto>>
    {
        
    }
    
    public class GetAllTeachersQueryHandler: IRequestHandler<GetAllTeachersQuery,List<TeacherViewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper; 

        public GetAllTeachersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TeacherViewDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _context.Teachers
                .Include(t => t.MainClassRoom)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<TeacherViewDto>>(teachers);
        }
    }
}