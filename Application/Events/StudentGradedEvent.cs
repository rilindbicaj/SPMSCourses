using System.Collections.Generic;
using Application.Enums;

namespace Application.Events
{
    public class StudentGradedEvent
    {
        public List<string> To { get; set; }
        public string StudentName { get; set; }
        public int Grade { get; set; }
        public string ProfessorName { get; set; }
        public string DateGraded { get; set; }
        public string Course { get; set; }
        public string EventType { get; set; }
    
    }
}