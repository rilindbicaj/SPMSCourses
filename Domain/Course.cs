using System.Collections.Generic;

namespace Domain
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Ects { get; set; }
        public int SemesterId { get; set; }
        public int SpecializationId { get; set; }
        public int CourseCategoryId {get; set; }
        
        public virtual ICollection<Courses_AcademicStaff> CoursesAcademicStaff { get; set; }
        public virtual CourseCategory CourseCategory { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual Semester Semester { get; set; }
    }
}