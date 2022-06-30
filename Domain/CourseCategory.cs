using System.Collections.Generic;

namespace Domain
{
    public class CourseCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public virtual ICollection<Course> Courses { get; set; }
    }
}