using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Courses
{
    public class GetCoursesForProfessor
    {
        public class Query : IRequest<Result<List<CourseResponse>>>
        {
        public int FacultyId { get; set; }
        public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<CourseResponse>>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<CourseResponse>>> Handle(Query request, CancellationToken token)
            {
                var courses = await _context.Courses.Where(c => c.Specialization.FacultyId == request.FacultyId
                                                                && c.CoursesAcademicStaff.Any(cas =>
                                                                    cas.AcademicStaff.UserId == request.UserId))
                    .ToListAsync();

                var responses = _mapper.Map<List<CourseResponse>>(courses);
                return ResultFactory.CreateSuccessfulResult<List<CourseResponse>>(responses);
            }
        }
    }
}