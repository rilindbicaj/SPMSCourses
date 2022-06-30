using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Grades;
using Application.Queries.Courses;
using Application.Queries.Grades;
using Application.Requests;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CoursesController : MediatorController
    {
        [HttpGet]

        public async Task<ActionResult<List<CourseResponse>>> GetAllCourses()
        {
            var response = await Mediator.Send(new GetAllCourses.Query());

            return HandleResult(response);
        }

        [HttpGet("forFaculty/{facultyId}")]

        public async Task<ActionResult<List<CourseResponse>>> GetCoursesForFaculty(int facultyId)
        {
            var response = await Mediator.Send(new GetCoursesForFaculty.Query { FacultyId = facultyId });
            
            return HandleResult(response);
        }
        
        [HttpPost("getRegisterableExams")]

        public async Task<ActionResult<List<CourseResponse>>> GetRegisterableExams(
            GetRegistrableExamsRequest getRegisterableExamsRequest
        )
        {
            var response = await Mediator.Send(new GetRegistrableExams.Query
                { GetRegistrableExamsRequest = getRegisterableExamsRequest });
            return HandleResult(response);
        }

    }
}