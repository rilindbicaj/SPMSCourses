using System;
using System.Collections.Generic;

namespace Domain
{
    public class Student
    {
        public Guid UserId { get; set; }
        public int FileNumber { get; set; }
        public string FullName { get; set; }
        
        public virtual ICollection<Grade> Grades { get; set; }
    }
}