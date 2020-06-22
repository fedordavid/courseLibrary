using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.Application.Queries.Courses
{
    internal class GetAllCourses : IExecuteQuery<GetAllCoursesQuery, QueryResult<CourseView>>
    {
        private readonly ICourseViews _courses;

        public GetAllCourses(ICourseViews courses) => _courses = courses;

        public Task<QueryResult<CourseView>> Execute(GetAllCoursesQuery query)
        {
            var queryResult = new QueryResult<CourseView>(_courses.Courses);
            
            return Task.FromResult(queryResult);
        }
    }
}