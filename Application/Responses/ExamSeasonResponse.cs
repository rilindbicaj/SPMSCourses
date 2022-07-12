using System;
using Domain;

namespace Application.Responses
{
    public class ExamSeasonResponse
    {
        public int ExamSeasonId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Faculty { get; set; }
        public ExamSeasonStatus Status { get; set; }
        
    }
}