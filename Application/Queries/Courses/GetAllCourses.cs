using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Responses;
using AutoMapper;
using MediatR;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Courses
{
    public class GetAllCourses
    {
        public class Query : IRequest<Result<List<CourseResponse>>>
        {
            
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
                
                var courses = await _context.Courses.Select(c => new Course()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    Ects = c.Ects,
                    CourseCategory = c.CourseCategory,
                    Specialization = c.Specialization,
                    Semester = c.Semester
                }).ToListAsync();

                var responses = _mapper.Map<List<CourseResponse>>(courses);
                
                return ResultFactory.CreateSuccessfulResult(responses);

            }
        }
    }
}