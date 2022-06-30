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
    public class CancelExamRegistration
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CancelExamRegistrationRequest CancelExamRegistrationRequest { get; set; }
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
                var grade = await _context.Grades.FindAsync(request.CancelExamRegistrationRequest.GradeId);
                
                if (grade == null) 
                    return ResultFactory.CreateFailedResult<Unit>("The specified grade does not exist");

                var examSeason = await _context.ExamSeasons.FindAsync(grade.ExamSeasonId);
                
                if(examSeason.StatusId != (int) ExamSeasonStatusTypes.Open)
                    return ResultFactory.CreateFailedResult<Unit>("Exam registration cannot be cancelled because the exam season it was registered in is no longer open.");
                
                if(grade.Student != request.CancelExamRegistrationRequest.StudentId)
                    return ResultFactory.CreateFailedResult<Unit>("Unauthorized access - the specified student has not registered the specified exam.");

                _context.Grades.Remove(grade);

                return await _context.SaveChangesAsync() > 0
                    ? ResultFactory.CreateSuccessfulResult(Unit.Value)
                    : ResultFactory.CreateFailedResult<Unit>("Could not update the database.");

            }
        }
    }
}