using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Enums;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Grades
{
    public class GenerateTranscript
    {
        public class Query : IRequest<Result<List<GradeResponse>>>
        {
            public GenerateTranscriptRequest GenerateTranscriptRequest { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<GradeResponse>>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<GradeResponse>>> Handle(Query request, CancellationToken token)
            {
                var transcript = await _context.Grades
                    .Where(g =>
                    g.StatusId == (int) GradeStatusTypes.Accepted &&
                    g.ExamSeason.Faculty == request.GenerateTranscriptRequest.FacultyId &&
                    g.StudentId == request.GenerateTranscriptRequest.StudentId)
                    .Select(g => new Grade
                    {
                        Value = g.Value,
                        CoursesAcademicStaff = new Courses_AcademicStaff
                        {
                            Course = new Course
                            {
                                CourseName = g.CoursesAcademicStaff.Course.CourseName,
                                CourseCode = g.CoursesAcademicStaff.Course.CourseCode,
                                CourseCategory = g.CoursesAcademicStaff.Course.CourseCategory,
                                Semester = g.CoursesAcademicStaff.Course.Semester,
                                Ects = g.CoursesAcademicStaff.Course.Ects
                            },
                            AcademicStaff = new AcademicStaff
                            {
                                FullName = g.CoursesAcademicStaff.AcademicStaff.FullName
                            }
                        }
                        
                    })
                    .ToListAsync();

                var response = _mapper.Map<List<GradeResponse>>(transcript);
                
                return ResultFactory.CreateSuccessfulResult(response);

            }
        }
    }
}