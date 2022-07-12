using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Queries.Grades
{
    public class GetCurrentExamSeason
    {
        public class Query : IRequest<Result<>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<>> Handle(Query request, CancellationToken token)
            {

            }
        }
    }
}