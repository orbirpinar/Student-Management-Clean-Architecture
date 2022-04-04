using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.Teacher.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Teacher.Queries
{
    public class GetTeacherByIdQuery: IRequest<TeacherViewDto>
    {
        public GetTeacherByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
    public class GetTeacherByIdQueryHandler: IRequestHandler<GetTeacherByIdQuery,TeacherViewDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeacherByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherViewDto> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            
            var teacher = await _context.Teachers.Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (teacher is null)
            {
                throw new NotFoundException(nameof(teacher), request.Id.ToString());
            }

            return _mapper.Map<TeacherViewDto>(teacher);
        }
    }
}