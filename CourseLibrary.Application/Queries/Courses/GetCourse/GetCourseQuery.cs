using System;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.Application.Queries.Courses
{
    public class GetCourseQuery : Query<CourseView>
    {
        public Guid AuthorId { get; set; } 
        
        public Guid CourseId { get; set; }
    }
}