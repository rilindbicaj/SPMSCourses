using System;
using Domain;

namespace Application.Responses
{
    public class GradeResponse
    {
        public int GradeId { get; set; }
        public int Value { get; set; }
        public DateTime DateGraded { get; set; }
        public GradeStatusResponse Status { get; set; }
        public CourseResponse Course { get; set; }
        public AcademicStaffResponse Professor { get; set; }
    }
}