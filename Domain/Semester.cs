using System.Collections.Generic;

namespace Domain
{
    public class Semester
    {
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        
        public virtual ICollection<Course> Courses { get; set; }
    }
}