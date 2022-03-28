
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Student.Responses;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Queries
{
    public class GetAllStudentQuery: IRequest<List<StudentResponse>>
    {
    }
    
    public class GetAllStudentQueryHandler: IRequestHandler<GetAllStudentQuery,List<StudentResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentResponse>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.Select(StudentResponse.Projection).ToListAsync(cancellationToken);
        }
    }
}