using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Authors;
using CourseLibrary.Application.Queries.Courses;
using CourseLibrary.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Persistence.Queries
{
    public class CourseLibraryQueryRepository : IAuthorViews, ICourseViews
    {
        private readonly CourseLibraryContext _context;
        private readonly IMapper _mapper;

        public IQueryable<AuthorView> Authors => _context.Authors.AsNoTracking().ProjectTo<AuthorView>(_mapper.ConfigurationProvider);
        
        public IQueryable<CourseView> Courses  => _context.Courses.AsNoTracking().ProjectTo<CourseView>(_mapper.ConfigurationProvider);

        public CourseLibraryQueryRepository(CourseLibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}