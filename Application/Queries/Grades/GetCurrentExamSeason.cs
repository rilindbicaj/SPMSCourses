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
    public class GetCurrentExamSeason
    {
        public class Query : IRequest<Result<ExamSeasonResponse>>
        {
            public int FacultyId { get; set; }
            
        }

        public class Handler : IRequestHandler<Query, Result<ExamSeasonResponse>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ExamSeasonResponse>> Handle(Query request, CancellationToken token)
            {
                var currentSeason = await _context.ExamSeasons.Where(e =>
                        e.Faculty == request.FacultyId
                        && (e.StatusId == (int) ExamSeasonStatusTypes.InProcess
                            || e.StatusId == (int) ExamSeasonStatusTypes.Open))
                    .Select(e => new ExamSeason
                    {
                        ExamSeasonId = e.ExamSeasonId,
                        Description = e.Description,
                        CurrentStatus = new ExamSeasonStatus
                        {
                            StatusId = e.CurrentStatus.StatusId,
                            StatusName = e.CurrentStatus.StatusName
                        },
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    })
                    .FirstOrDefaultAsync();
                
                if(currentSeason == null)
                    return ResultFactory.CreateFailedResult<ExamSeasonResponse>("No exam season is open or in process right now");

                var res = _mapper.Map<ExamSeasonResponse>(currentSeason);
                
                return ResultFactory.CreateSuccessfulResult(res);
            }
        }
    }
}