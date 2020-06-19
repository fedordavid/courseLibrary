using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.Application.Queries.Courses
{
    internal class GetCourses : IExecuteQuery<GetCoursesQuery, QueryResult<CourseView>>
    {
        private readonly ICourseViews _courses;

        public GetCourses(ICourseViews courses) => _courses = courses;

        public Task<QueryResult<CourseView>> Execute(GetCoursesQuery query)
        {
            var views = _courses.Courses
                .Where(c => c.AuthorId == query.AuthorId)
                .OrderBy(c => c.Title);

            var queryResult = new QueryResult<CourseView>(views);
            
            return Task.FromResult(queryResult);
        }
    }
}