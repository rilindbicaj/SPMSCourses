using System;
using System.Collections.Generic;

namespace Domain
{
    public class AcademicStaff
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
            
        public virtual ICollection<Courses_AcademicStaff> CoursesAcademicStaff { get; set; }
        
    }
}