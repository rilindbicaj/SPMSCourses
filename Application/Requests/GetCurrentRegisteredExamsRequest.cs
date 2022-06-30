using System;

namespace Application.Requests
{
    public class GetCurrentRegisteredExamsRequest
    {
        public Guid StudentId { get; set; }
        public int FacultyId { get; set; }
        
    }
}