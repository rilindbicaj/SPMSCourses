using System;

namespace Application.Requests
{
    public class GenerateTranscriptRequest
    {
        public Guid StudentId { get; set; }
        public int FacultyId { get; set; }
    }
}