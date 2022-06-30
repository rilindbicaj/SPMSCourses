using System.Collections.Generic;

namespace Domain
{
    public class GradeStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        
        public virtual  ICollection<Grade> Grades {get; set; }
    }
}