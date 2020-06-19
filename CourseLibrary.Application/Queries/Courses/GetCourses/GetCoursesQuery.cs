using System;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.Application.Queries.Courses
{
    public class GetCoursesQuery : Query<QueryResult<CourseView>>
    {
        public Guid AuthorId { get; }

        public GetCoursesQuery(Guid authorId)
        {
            AuthorId = authorId;
        }
    }
}