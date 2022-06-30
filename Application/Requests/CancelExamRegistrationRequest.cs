using System;

namespace Application.Requests
{
    public class CancelExamRegistrationRequest
    {
        public int GradeId { get; set; }
        public Guid StudentId { get; set; }
    }
}