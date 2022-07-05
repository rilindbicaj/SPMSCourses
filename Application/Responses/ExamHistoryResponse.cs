using System;
using System.Collections.Generic;
using Domain;

namespace Application.Responses
{
    public class ExamHistoryResponse
    {
        public int ExamSeasonId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ExamSeasonStatus CurrentStatus { get; set; }
        public ICollection<GradeResponse> Grades { get; set; }
    }
}