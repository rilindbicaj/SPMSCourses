using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Enums;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries.Grades
{
    public class GenerateExamHistory
    {
        public class Query : IRequest<Result<ICollection<ExamHistoryResponse>>>
        {
            public int FacultyId { get; set; }
            public Guid StudentId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ICollection<ExamHistoryResponse>>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ICollection<ExamHistoryResponse>>> Handle(Query request, CancellationToken token)
            {

                var examSeasons = await _context.ExamSeasons.Where(e =>
                        e.Grades.Any(g => g.StudentId == request.StudentId)
                        && e.StatusId == (int)ExamSeasonStatusTypes.Closed
                        && e.Faculty == request.FacultyId)
                    .Select(e => new ExamSeason
                    {
                        ExamSeasonId = e.ExamSeasonId,
                        Description = e.Description,
                        Grades = e.Grades.Where(g => g.StudentId == request.StudentId)
                            .Select(g => new Grade
                            {
                                GradeId = g.GradeId,
                                Value = g.Value,
                                DateGraded = g.DateGraded,
                                Status = g.Status,
                                CoursesAcademicStaff = new Courses_AcademicStaff
                                {
                                    Course = g.CoursesAcademicStaff.Course,
                                    AcademicStaff = g.CoursesAcademicStaff.AcademicStaff
                                }
                            }).ToList(),
                        CurrentStatus = e.CurrentStatus,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    })
                    .ToListAsync();

                var histories = _mapper.Map<ICollection<ExamHistoryResponse>>(examSeasons);

                return ResultFactory.CreateSuccessfulResult(histories);

            }
        }
    }
}