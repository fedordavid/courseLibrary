using System;
using System.Collections.Generic;
using System.Linq;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.ResourceParameters;

namespace CourseLibrary.Application.Queries
{
    public class CourseLibraryQueryService : ICourseLibraryQueryService
    {
        private readonly ICourseLibraryQueryRepository _repository;

        public CourseLibraryQueryService(ICourseLibraryQueryRepository repository )
        {
            _repository = repository;
        }

        public Course GetCourse(GetCourseQuery query)
        {
            return _repository.Courses
                .FirstOrDefault(c => c.AuthorId == query.AuthorId && c.Id == query.CourseId);
        }

        public Course[] GetCourses(Guid authorId)
        {
            return _repository.Courses
                .Where(c => c.AuthorId == authorId)
                .OrderBy(c => c.Title)
                .ToArray();
        }

        public bool AuthorExists(Guid authorId)
        {
            return _repository.Authors.Any(a => a.Id == authorId);
        }

        public Author GetAuthor(Guid authorId)
        {
            return _repository.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public Author[] GetAuthors(GetAuthorsQuery getAuthorsQuery = null)
        {
            var queryable = _repository.Authors;

            if (getAuthorsQuery != null && !string.IsNullOrWhiteSpace(getAuthorsQuery.MainCategory))
            {
                queryable = queryable.Where(a => a.MainCategory == getAuthorsQuery.MainCategory);
            }

            if (getAuthorsQuery != null && !string.IsNullOrWhiteSpace(getAuthorsQuery.SearchQuery))
            {
                queryable = queryable.Where(a => a.MainCategory.Contains(getAuthorsQuery.SearchQuery)
                    || a.FirstName.Contains(getAuthorsQuery.SearchQuery)
                    || a.LastName.Contains(getAuthorsQuery.SearchQuery));
            }

            return queryable.ToResult();
        }

        public Author[] GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _repository.Authors.Where(a => authorIds.Contains(a.Id)).ToResult();
        }
    }
    
    public static class AuthorQueryableExtensions
    {
        public static Author[] ToResult(this IQueryable<Author> authors)
        {
            return authors
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToArray();
        }
    }
}