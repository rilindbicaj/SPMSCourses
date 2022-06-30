using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/coursesservice/[controller]")]
    public class MediatorController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Exception to be handled here");

            if (result.IsSuccess && result.Value == null) return NotFound("Request is OK, but the required resource was not found");
            
            if (result.IsSuccess) return Ok(result.Value);

            return BadRequest(result.Error);

        }

    }
}