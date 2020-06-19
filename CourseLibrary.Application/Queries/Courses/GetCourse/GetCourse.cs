using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Core;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Application.Queries.Courses
{
    public class GetCourse : IExecuteQuery<GetCourseQuery, CourseView>
    {
        private readonly ICourseViews _courses;

        public GetCourse(ICourseViews courses) => _courses = courses;

        public Task<CourseView> Execute(GetCourseQuery query)
        {
            return _courses.Courses
                .FirstOrDefaultAsync(c => c.AuthorId == query.AuthorId && c.Id == query.CourseId);
        }
    }
}