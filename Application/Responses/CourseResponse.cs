using System.Collections.Generic;

namespace Application.Responses
{
    public class CourseResponse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int Ects { get; set; }
        public SemesterResponse Semester { get; set; }
        public SpecializationResponse Specialization { get; set; }
        public CourseCategoryResponse CourseCategory { get; set; }
        
        public ICollection<AcademicStaffResponse> Lecturers { get; set; }
    }
}