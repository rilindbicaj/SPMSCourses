using System;
using System.Collections.Generic;

namespace Domain
{
    public class ExamSeason
    {
        public int ExamSeasonId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Faculty { get; set; }
        public int StatusId { get; set; }
        public virtual ExamSeasonStatus CurrentStatus { get; set; }
        
        public virtual ICollection<Grade> Grades { get; set; }

    }
}