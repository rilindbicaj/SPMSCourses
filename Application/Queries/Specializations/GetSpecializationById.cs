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
    public class GetSpecializationById
    {
        public class Query : IRequest<Result<SpecializationResponse>>
        {
            public int SpecializationId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SpecializationResponse>>
        {
            private readonly SPMSCoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(SPMSCoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<SpecializationResponse>> Handle(Query request, CancellationToken token)
            {
                var specialization = await _context.Specializations.FindAsync(request.SpecializationId);
                var response = _mapper.Map<SpecializationResponse>(specialization);

                return ResultFactory.CreateSuccessfulResult(response);

            }
        }
    }
}