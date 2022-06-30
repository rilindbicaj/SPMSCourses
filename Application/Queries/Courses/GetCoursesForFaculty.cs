using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Courses
{
    public class GetCoursesForFaculty
    {
        public class Query : IRequest<Result<List<CourseResponse>>>
        {
        public int FacultyId { get; set; }
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
                
                if(!_context.Specializations.Any(x => x.FacultyId == request.FacultyId)) 
                    return ResultFactory.CreateFailedResult<List<CourseResponse>>("No faculty with such an ID exists.");
                
                var courses = await _context.Courses
                    .Where(x => x.Specialization.FacultyId == request.FacultyId)
                    .Select(c => new Course()
                    {
                        CourseId = c.CourseId,
                        CourseName = c.CourseName,
                        CourseCode = c.CourseCode,
                        Ects = c.Ects,
                        CourseCategory = c.CourseCategory,
                        Specialization = c.Specialization,
                        Semester = c.Semester
                    }).ToListAsync();

                var response = _mapper.Map<List<CourseResponse>>(courses);

                response = !response.Any() ? null : response;
                
                return ResultFactory.CreateSuccessfulResult(response);

            }
        }
    }
}