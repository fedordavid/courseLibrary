using System;

namespace CourseLibrary.Application.Queries
{
    public class GetCourseQuery
    {
        public Guid AuthorId { get; set; } 
        public Guid CourseId { get; set; }
    }
}