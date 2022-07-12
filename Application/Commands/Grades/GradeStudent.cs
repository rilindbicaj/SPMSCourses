using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Enums;
using Application.EventPublisher;
using Application.Events;
using Application.Requests;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Commands.Grades
{
    public class GradeStudent
    {
        public class Command : IRequest<Result<Unit>>
        {
            public GradeStudentRequest GradeStudentRequest { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;
            private readonly IEventPublisher _publisher;

            public Handler(SPMSCoursesContext context, IMapper mapper, IEventPublisher publisher)
            {
                _context = context;
                _mapper = mapper;
                _publisher = publisher;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken token)
            {

                var grade = await _context.Grades.FindAsync(request.GradeStudentRequest.GradeId);
                
                if (grade == null) return ResultFactory.CreateFailedResult<Unit>("The specified grade does not exist.");
                
                if (grade.AcademicStaffId != request.GradeStudentRequest.StaffId)
                    return ResultFactory.CreateFailedResult<Unit>("The specified lecturer is not the one the exam was registered for.");

                var examSeason = await _context.ExamSeasons.FindAsync(grade.ExamSeasonId);
                
                if (examSeason.StatusId != (int) ExamSeasonStatusTypes.InProcess) 
                    return ResultFactory.CreateFailedResult<Unit>("The exam season the exam was registered in is either closed or still open.");

                grade.Value = request.GradeStudentRequest.Value;
                grade.DateGraded = DateTime.Now;

                if (grade.Value == 0) grade.StatusId = (int) GradeStatusTypes.Abstained;

                _context.Entry(grade).State = EntityState.Modified;

                var eventData = _mapper.Map<StudentGradedEvent>(await GetEventForGrade(grade.GradeId));
                
                _publisher.PublishStudentGradedEvent(eventData);
                
                Console.WriteLine(eventData);
                
                return await _context.SaveChangesAsync() > 0 ? 
                    ResultFactory.CreateSuccessfulResult(Unit.Value) : 
                    ResultFactory.CreateFailedResult<Unit>("There was a problem saving changes to the database.");
            }

            private async Task<Grade> GetEventForGrade(int gradeId)
            {
                return await _context.Grades
                    .Where(g => g.GradeId == gradeId)
                    .Include(g => g.Student)
                    .Include(g => g.CoursesAcademicStaff)
                    .ThenInclude(g => g.AcademicStaff)
                    .Include(g => g.CoursesAcademicStaff)
                    .ThenInclude(g => g.Course)
                    .FirstOrDefaultAsync();
            }

        }
    }
}