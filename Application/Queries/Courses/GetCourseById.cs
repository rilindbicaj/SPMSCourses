using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Core.Factories;
using Application.Responses;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Queries.Courses
{
    public class GetCourseById
    {
        public class Query : IRequest<Result<CourseResponse>>
        {
            public int CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CourseResponse>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CourseResponse>> Handle(Query request, CancellationToken token)
            {
                var course = await _context.Courses.FindAsync(request.CourseId);

                var response = _mapper.Map<CourseResponse>(course);

                return ResultFactory.CreateSuccessfulResult(response);

            }
        }
    }
}