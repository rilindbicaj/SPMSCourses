using System;
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
    /*
     * Supplied with the ID of the desired faculty and student, returns all the exams the student has registered in the current exam season
     * Exam season can be opened or in process. 
     */
    public class GetCurrentRegisteredExams
    {
        public class Query : IRequest<Result<List<GradeResponse>>>
        {
           public GetCurrentRegisteredExamsRequest GetCurrentRegisteredExamsRequest { get; set; }
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

                var currentlyOpenedSeason =
                    await GetCurrenlyOpenedExamSeason(request.GetCurrentRegisteredExamsRequest.FacultyId);
                
                if (currentlyOpenedSeason == null) 
                    return ResultFactory.CreateFailedResult<List<GradeResponse>>("No exam season is open or in process for that faculty");

                var registeredExams =
                    await GetCurrentlyRegisteredGrades(request.GetCurrentRegisteredExamsRequest.StudentId,
                        currentlyOpenedSeason.ExamSeasonId);
                
                var response = _mapper.Map<List<GradeResponse>>(registeredExams);
                
                return ResultFactory.CreateSuccessfulResult(response);
            }

            /*
             * Gets the exams the given student has registered in the given exam season. 
             */
            private async Task<List<Grade>> GetCurrentlyRegisteredGrades(Guid studentId, int examSeasonId)
            {
                return await _context.Grades.Where(g =>
                        g.StudentId == studentId
                        && g.ExamSeasonId == examSeasonId)
                    .Select(g => 
                        new Grade {
                            GradeId = g.GradeId,
                            DateGraded = g.DateGraded,
                            CoursesAcademicStaff = new Courses_AcademicStaff
                            {
                                Course = g.CoursesAcademicStaff.Course,
                                AcademicStaff = g.CoursesAcademicStaff.AcademicStaff
                            },
                            Status = g.Status,
                            Value = g.Value,
                        })
                    .ToListAsync();
                
            }
            
            /*
             * Gets the currently opened exam season for the specified faculty.
             * An exam season is considered open, for this use case,
             * if it's either in process (exams are taking place or open for registrations.
             */

            private async Task<ExamSeason> GetCurrenlyOpenedExamSeason(int facultyId)
            {
                return await _context.ExamSeasons.Where(e =>
                        e.Faculty == facultyId
                        && (e.StatusId == (int) ExamSeasonStatusTypes.InProcess
                            || e.StatusId == (int) ExamSeasonStatusTypes.Open))
                    .FirstOrDefaultAsync();
            }
            
        }
    }
}