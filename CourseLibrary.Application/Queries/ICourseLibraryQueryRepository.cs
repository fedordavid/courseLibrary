using System.Linq;
using CourseLibrary.Application.Entities;

namespace CourseLibrary.Application.Queries
{
    public interface ICourseLibraryQueryRepository
    {
        IQueryable<Author> Authors { get; }
        IQueryable<Course> Courses { get; }
    }
}