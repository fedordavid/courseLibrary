using System.Linq;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.Queries;
using CourseLibrary.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Persistence.Repositories
{
    public class CourseLibraryQueryRepository : ICourseLibraryQueryRepository
    {
        private readonly CourseLibraryContext _context;
        public IQueryable<Author> Authors => _context.Authors.AsNoTracking();
        public IQueryable<Course> Courses  => _context.Courses.AsNoTracking();

        public CourseLibraryQueryRepository(CourseLibraryContext context)
        {
            _context = context;
        }
    }
}