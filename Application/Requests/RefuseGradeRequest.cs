using System;

namespace Application.Requests
{
    public class RefuseGradeRequest
    {
        public int GradeId { get; set; }
        public Guid UserId { get; set; }
    }
}