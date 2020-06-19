using System;
using System.Linq;

namespace CourseLibrary.Application.Queries.Courses
{
    public class CourseView
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }
    }

    public interface ICourseViews
    {
        IQueryable<CourseView> Courses { get; }
    }
}