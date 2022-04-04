using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Student.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Student.Queries
{
    public class GetStudentByIdQuery: IRequest<StudentWithClassRoomViewDto>
    {
        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
    public class GetStudentByIdQueryHandler: IRequestHandler<GetStudentByIdQuery,StudentWithClassRoomViewDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentWithClassRoomViewDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student =  await _context.Students.AsNoTracking()
                .Where(s => s.Id == request.Id)
                .Include(s => s.ClassRoom)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (student is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.Id.ToString());
            }

            return _mapper.Map<StudentWithClassRoomViewDto>(student);
        }
    }
}