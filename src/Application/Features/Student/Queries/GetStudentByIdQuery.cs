using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Student.Responses;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Queries
{
    public class GetStudentByIdQuery: IRequest<StudentResponse>
    {
        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
    public class GetStudentByIdQueryHandler: IRequestHandler<GetStudentByIdQuery,StudentResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentResponse> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var studentResponse =  await _context.Students.AsNoTracking()
                .Where(s => s.Id == request.Id)
                .Select(StudentResponse.Projection)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (studentResponse is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.Id.ToString());
            }

            return studentResponse;
        }
    }
}