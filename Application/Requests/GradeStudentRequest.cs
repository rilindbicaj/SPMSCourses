using System;

namespace Application.Requests
{
    public class GradeStudentRequest
    {
        public int GradeId { get; set; }
        public int Value { get; set; }
        
        public Guid StaffId { get; set; }
    }
}