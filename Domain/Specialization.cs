using System.Collections.Generic;

namespace Domain
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public int FacultyId { get; set; }
        public int ParentSpecializationId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual Specialization ParentSpecialization { get; set; }
        
        public virtual ICollection<Specialization> ChildSpecializations { get; set; } 
    }
}