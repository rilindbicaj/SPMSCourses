using System.Threading.Tasks;
using Application.Queries.Grades;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SpecializationsController : MediatorController
    {

        [HttpGet("{specializationId}")]
        public async Task<ActionResult<SpecializationResponse>> GetById(int specializationId)
        {

            var response = await Mediator.Send(new GetSpecializationById.Query { SpecializationId = specializationId });
            return HandleResult(response);

        }
    }
}