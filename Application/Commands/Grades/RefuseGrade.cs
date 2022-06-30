using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Enums;
using Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Commands.Grades
{
    public class RefuseGrade
    {
        public class Command : IRequest<Result<Unit>>
        {
            public RefuseGradeRequest RefuseGradeRequest { get; set; }
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

                var grade = await _context.Grades.FindAsync(request.RefuseGradeRequest.GradeId);

                if (grade == null) 
                    return ResultFactory.CreateFailedResult<Unit>("The specified grade does not exist");

                if (grade.Value == -1 || grade.DateGraded == null)
                    return ResultFactory.CreateFailedResult<Unit>("Cannot refuse a grade that hasn't been graded yet.");
                
                if(grade.Student != request.RefuseGradeRequest.UserId)
                    return ResultFactory.CreateFailedResult<Unit>("The specified student has not registered the exam the specified grade represents.");
                
                if(DateTime.Now.Subtract(grade.DateGraded).TotalSeconds > 172800) // Two days, in seconds; checks if it's been more than 2 days since the date of grading
                    return ResultFactory.CreateFailedResult<Unit>("Grade cannot be refused 48 hours after it's been graded.");

                grade.StatusId = (int)GradeStatusTypes.Refused;

                _context.Entry(grade).State = EntityState.Modified;

                return await _context.SaveChangesAsync() > 0
                    ? ResultFactory.CreateSuccessfulResult(Unit.Value)
                    : ResultFactory.CreateFailedResult<Unit>("Problems saving changes to database");

            }
        }
    }
}