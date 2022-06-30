using System;
using System.Collections.Generic;

namespace Domain
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Value { get; set; }
        public DateTime DateGraded { get; set; }
        public Guid Student { get; set; }
        public int StatusId { get; set; }
        public int CourseId { get; set; }
        public Guid AcademicStaffId { get; set; }
        public int ExamSeasonId { get; set; }
        
        public virtual ExamSeason ExamSeason { get; set; }
        
        public virtual GradeStatus Status { get; set; }
        public virtual Courses_AcademicStaff CoursesAcademicStaff { get; set; }
        
        
    }
}