using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Enums;
using Application.Requests;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Commands.Grades
{
    public class RegisterExam
    {
        public class Command : IRequest<Result<Unit>>
        {
            public RegisterExamRequest RegisterExamRequest { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly SPMSCoursesContext _context;

            public Handler(SPMSCoursesContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken token)
            {
                var currentlyOpenedSeason = await GetCurrenlyOpenedExamSeason(request.RegisterExamRequest.FacultyId);
                
                if(currentlyOpenedSeason == null)
                    return ResultFactory.CreateFailedResult<Unit>("There is no exam season currently opened for the specified faculty.");

                if(await ExamAlreadyRegistered(
                       request.RegisterExamRequest.CourseId,
                       request.RegisterExamRequest.LecturerId,
                       request.RegisterExamRequest.StudentId))
                    return ResultFactory.CreateFailedResult<Unit>("Student has already registered this exam before.");
                
                var grade = new Grade
                {
                    CourseId = request.RegisterExamRequest.CourseId,
                    AcademicStaffId = request.RegisterExamRequest.LecturerId,
                    StatusId = (int)GradeStatusTypes.Pending,
                    Value = -1,
                    StudentId = request.RegisterExamRequest.StudentId,
                    ExamSeasonId = currentlyOpenedSeason.ExamSeasonId
                };
                
                await _context.AddAsync(grade);

                return await _context.SaveChangesAsync() > 0
                    ? ResultFactory.CreateSuccessfulResult(Unit.Value)
                    : ResultFactory.CreateFailedResult<Unit>("Problem saving changes to database");
                
            }
            
            private async Task<ExamSeason> GetCurrenlyOpenedExamSeason(int facultyId)
            {
                return await _context.ExamSeasons.Where(e =>
                        e.Faculty == facultyId
                        && e.StatusId == (int) ExamSeasonStatusTypes.InProcess)
                    .FirstOrDefaultAsync();
            }

            private async Task<bool> ExamAlreadyRegistered(int courseId, Guid academicStaffId, Guid studentId)
            {
                return await _context.Grades.Where(g =>
                        g.StudentId == studentId 
                        && g.CourseId == courseId
                        && g.AcademicStaffId == academicStaffId
                        && g.StatusId < 3)
                    .FirstOrDefaultAsync() != null;
            }

        }
    }
}