using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Authors;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Application.Queries
{
    public class CourseLibraryQueryService : ICourseLibraryQueryService
    {
        private readonly IAuthorViews _authors;

        public CourseLibraryQueryService(IAuthorViews authors)
        {
            _authors = authors;
        }

        public Task<bool> AuthorExists(Guid authorId)
        {
            return _authors.Authors.AnyAsync(a => a.Id == authorId);
        }

        public Task<AuthorView> GetAuthor(Guid authorId)
        {
            return _authors.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public Task<AuthorView[]> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _authors.Authors.Where(a => authorIds.Contains(a.Id)).ToResult();
        }
    }
    
    public static class AuthorQueryableExtensions
    {
        public static Task<AuthorView[]> ToResult(this IQueryable<AuthorView> authors)
        {
            return authors
                .OrderBy(a => a.Name)
                .ToArrayAsync();
        }
    }
}