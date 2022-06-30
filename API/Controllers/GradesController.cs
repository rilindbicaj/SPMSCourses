using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Grades;
using Application.Queries.Grades;
using Application.Requests;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GradesController : MediatorController
    {
        
        [HttpPost("getCurrentlyRegisteredExams")]

        public async Task<ActionResult<List<GradeResponse>>> GetCurrentlyRegisteredExams(
            GetCurrentRegisteredExamsRequest getCurrentRegisteredExamsRequest)
        {
            var response = await Mediator.Send(new GetCurrentRegisteredExams.Query
                { GetCurrentRegisteredExamsRequest = getCurrentRegisteredExamsRequest });
            return HandleResult(response);
        }

        [HttpPut("gradeStudent")]

        public async Task<ActionResult> GradeStudent(GradeStudentRequest gradeStudentRequest)
        {
            var response = await Mediator.Send(new GradeStudent.Command { GradeStudentRequest = gradeStudentRequest });
            return HandleResult(response);
        }

        [HttpPut("refuseGrade")]

        public async Task<ActionResult> RefuseGrade(RefuseGradeRequest refuseGradeRequest)
        {
            var response = await Mediator.Send(new RefuseGrade.Command { RefuseGradeRequest = refuseGradeRequest });
            return HandleResult(response);
        }

        [HttpDelete("cancelExamRegistration")]

        public async Task<ActionResult> CancelExamRegistration(
            CancelExamRegistrationRequest cancelExamRegistrationRequest)
        {
            var response = await Mediator.Send(new CancelExamRegistration.Command
                { CancelExamRegistrationRequest = cancelExamRegistrationRequest });
            return HandleResult(response);
        }

        [HttpGet("generateTranscript/{facultyId}/{studentId}")]

        public async Task<ActionResult<List<GradeResponse>>> GenerateTranscript(
            int facultyId, Guid studentId)
        {
            var response = await Mediator.Send(new GenerateTranscript.Query
                { GenerateTranscriptRequest = new GenerateTranscriptRequest { FacultyId = facultyId, StudentId = studentId} });
            return HandleResult(response);
        }

    }
}