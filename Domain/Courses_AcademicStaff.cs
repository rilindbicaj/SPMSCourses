using System;
using System.Collections.Generic;

namespace Domain
{
    public class Courses_AcademicStaff
    {
        public int CourseId { get; set; }
        public Guid AcademicStaffId { get; set; }
        public int LectureRoleId { get; set; }
        
        public virtual Course Course { get; set; }
        public virtual AcademicStaff AcademicStaff { get; set; }
        public virtual LectureRole LectureRole { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}