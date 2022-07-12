using System;

namespace Application.Requests
{
    public class GetRegistrableExamsRequest
    {
        public Guid StudentId { get; set; }
        public int SpecializationId { get; set; }
        public int CurrentSemesterId { get; set; }
    }
}