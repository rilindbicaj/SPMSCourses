using System.Collections.Generic;

namespace Domain
{
    public class LectureRole
    {
        
        public int LectureRoleId { get; set; }
        public string RoleName { get; set; }
        
        public virtual ICollection<Courses_AcademicStaff> CoursesAcademicStaves { get; set; }

    }
}