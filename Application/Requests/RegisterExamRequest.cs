using System;

namespace Application.Requests
{
    public class RegisterExamRequest
    {
        public int CourseId { get; set; }
        public Guid LecturerId { get; set; }
        public Guid StudentId { get; set; }
        public int FacultyId { get; set; }
    }
}