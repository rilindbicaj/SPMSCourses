using System;

namespace Application.Responses
{
    public class StudentResponse
    {
        public Guid UserId { get; set; }
        public int FileNumber {get; set; }
        public string FullName { get; set; }
    }
}