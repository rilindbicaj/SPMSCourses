using System.Collections.Generic;

namespace Domain
{
    public class ExamSeasonKind
    {
        public int ExamSeasonKindId { get; set; }
        public string ExamSeasonKindName { get; set; }
        
        public virtual ICollection<ExamSeason> ExamSeasons { get; set; }
    }
}