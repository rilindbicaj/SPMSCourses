using System.Collections.Generic;

namespace Domain
{
    public class ExamSeasonStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        
        public virtual ICollection<ExamSeason> ExamSeasons { get; set; }
    }
}