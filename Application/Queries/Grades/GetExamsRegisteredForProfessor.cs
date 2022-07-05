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
    public class GetExamsRegisteredForProfessor
    {
        public class Query : IRequest<Result<List<GradeResponse>>>
        {
            public int FacultyId { get; set; }
            public int CourseId { get; set; }
            public Guid LecturerId { get; set; }
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
                var currentlyOpenedExam = await GetCurrenlyOpenedExamSeason(request.FacultyId);
                
                Console.WriteLine(currentlyOpenedExam.Description);
                
                var grades = await _context.Grades
                    .Where(g => g.ExamSeasonId == currentlyOpenedExam.ExamSeasonId
                    //&& g.CourseId == request.CourseId
                    && g.AcademicStaffId == request.LecturerId)
                    .ToListAsync();

                var res = _mapper.Map<List<GradeResponse>>(grades);
                
                return ResultFactory.CreateSuccessfulResult(res);
            }
            
            private async Task<ExamSeason> GetCurrenlyOpenedExamSeason(int facultyId)
            {
                return await _context.ExamSeasons.Where(e =>
                        e.Faculty == facultyId
                        && e.StatusId == (int) ExamSeasonStatusTypes.InProcess)
                    .FirstOrDefaultAsync();
            }
        }
    }
}