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
    public class GetRegistrableExams
    {
        public class Query : IRequest<Result<List<CourseResponse>>>
        {
            public GetRegistrableExamsRequest GetRegistrableExamsRequest { get; set; }
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
                
                var specialization = await _context.Specializations.Where(e =>
                    e.SpecializationId == request.GetRegistrableExamsRequest.SpecializationId).FirstOrDefaultAsync();
                
                if(specialization == null) 
                    return ResultFactory.CreateFailedResult<List<CourseResponse>>("Specified specialization does not exist");

                var currentlyOpenedSeason = await GetCurrenlyOpenedExamSeason(specialization.FacultyId);
                
                if(currentlyOpenedSeason == null)
                    return ResultFactory.CreateFailedResult<List<CourseResponse>>("There currently isn't an open exam season for the specified faculty.");

                if (!CurrentSeasonIsRegular(currentlyOpenedSeason.SeasonKind.ExamSeasonKindId))
                    request.GetRegistrableExamsRequest.CurrentSemesterId--;
                
                var registrableExams = await GetExamsStudentCanRegister(
                    specialization, request.GetRegistrableExamsRequest.CurrentSemesterId, request.GetRegistrableExamsRequest.StudentId);
                
                var response = _mapper.Map<List<CourseResponse>>(registrableExams);

                return ResultFactory.CreateSuccessfulResult(response);

            }
            
            /**
             * Gets all the exams the specified student can register, considering the specialization they are in,
             * and the current semester they're attending. This method also returns exams for courses they've refused
             * the grade for, or abstained in the past.
             */

            private async Task<List<Course>> GetExamsStudentCanRegister(Specialization specialization, int currentSemesterId, Guid studentId)
            {
                return await _context.Courses.Where(c =>
                        (c.SpecializationId == specialization.SpecializationId
                         || c.SpecializationId == specialization.ParentSpecializationId)
                        && c.SemesterId <= currentSemesterId
                        && !_context.Grades.Any(g =>
                                (g.StudentId == studentId && g.CourseId == c.CourseId && g.StatusId < 3) // Do not take exams that the student has taken and accepted / pending
                        ))
                    .Select(c => new Course
                    {
                        CourseId = c.CourseId,
                        CourseName = c.CourseName,
                        CourseCategory = c.CourseCategory,
                        CourseCode = c.CourseCode,
                        Ects = c.Ects,
                        CoursesAcademicStaff = _context.CoursesAcademicStaff.
                            Where(cas => cas.CourseId == c.CourseId && cas.LectureRoleId == 1)
                            .Include(x=>x.AcademicStaff)
                            .ToList(),
                        Semester = c.Semester,
                        Specialization = c.Specialization
                    })
                    .ToListAsync();
            }

            /**
             * Gets the currently opened exam season for the specified faculty.
             * An exam season is considered open, for this use case,
             * if it's either in process (exams are taking place or open for registrations.
             */

            private async Task<ExamSeason> GetCurrenlyOpenedExamSeason(int facultyId)
            {
                return await _context.ExamSeasons.Where(e =>
                        e.Faculty == facultyId
                        && 
                        // (e.StatusId == (int) ExamSeasonStatusTypes.InProcess ||
                             e.StatusId == (int) ExamSeasonStatusTypes.Open)
                    .Include(e => e.SeasonKind)
                    .FirstOrDefaultAsync();
            }

            /// <summary>
            /// Checks whether the provided ExamSeasonKind is a regular registering season or an irregular one.
            /// </summary>
            /// <param name="examSeasonKindId">The ID of the ExamSeasonKind object</param>
            /// <returns>True if the season kind is regular, false otherwise.</returns>
            private bool CurrentSeasonIsRegular(int examSeasonKindId)
            {
                return examSeasonKindId is (int) ExamSeasonKindsEnum.FEBRUARY
                    or (int) ExamSeasonKindsEnum.JUNE
                    or (int) ExamSeasonKindsEnum.SEPTEMBER;
            }
            
        }
    }
}