using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Grades;
using Application.Queries.Grades;
using Application.Requests;
using Application.Responses;
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

        [HttpDelete("cancelExamRegistration/{gradeId}/{studentId}")]

        public async Task<ActionResult> CancelExamRegistration(
            int gradeId, Guid studentId)
        {
            var response = await Mediator.Send(new CancelExamRegistration.Command
            {
                CancelExamRegistrationRequest = new CancelExamRegistrationRequest
                {
                    GradeId = gradeId,
                    StudentId = studentId
                }
            });
            return HandleResult(response);
        }

        [HttpGet("generateTranscript/{facultyId}/{studentId}")]

        public async Task<ActionResult<List<GradeResponse>>> GenerateTranscript(
            int facultyId, Guid studentId)
        {
            var response = await Mediator.Send(new GenerateTranscript.Query
            { GenerateTranscriptRequest = new GenerateTranscriptRequest { FacultyId = facultyId, StudentId = studentId } });
            return HandleResult(response);
        }

        [HttpPost("registerExam")]
        public async Task<ActionResult> RegisterExam(RegisterExamRequest registerExamRequest)
        {
            var response = await Mediator.Send(new RegisterExam.Command { RegisterExamRequest = registerExamRequest });
            return HandleResult(response);
        }

        [HttpGet("examHistory/{facultyId}/{studentId}")]

        public async Task<ActionResult<List<ExamHistoryResponse>>> GetExamHistory(int facultyId, Guid studentId)
        {
            var response = await Mediator.Send(new GenerateExamHistory.Query
            { FacultyId = facultyId, StudentId = studentId });
            return HandleResult(response);
        }

        [HttpGet("examsRegisteredForProfessor/{facultyId}/{lecturerId}")]

        public async Task<ActionResult<List<GradeResponse>>> GetExamsRegisteredForProfessor(int facultyId,
            Guid lecturerId)
        {
            var response = await Mediator.Send(new GetExamsRegisteredForProfessor.Query
            {
                FacultyId = facultyId,
                LecturerId = lecturerId
            });
            return HandleResult(response);
        }

        [HttpGet("currentlyOpenedExamSeason/{facultyId}")]
        public async Task<ActionResult<ExamSeasonResponse>> GetCurrentlyOpenedOrInProcessExamSeaoson(int facultyId)
        {
            var response = await Mediator.Send(new GetCurrentExamSeason.Query { FacultyId = facultyId });
            return HandleResult(response);
        }

    }
}