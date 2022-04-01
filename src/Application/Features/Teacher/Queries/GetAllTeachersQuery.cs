using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Mapping;
using Application.Interfaces;
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

        public GetAllTeachersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<TeacherViewDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            return _context.Teachers.Select(t => t.ToViewDto()).ToListAsync(cancellationToken);
        }
    }
}