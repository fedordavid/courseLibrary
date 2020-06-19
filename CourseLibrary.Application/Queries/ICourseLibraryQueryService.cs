using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Authors;
using CourseLibrary.Application.Queries.Courses;

namespace CourseLibrary.Application.Queries
{
    public interface ICourseLibraryQueryService
    {
        
        Task<AuthorView> GetAuthor(Guid authorId);
        
        Task<AuthorView[]> GetAuthors(IEnumerable<Guid> authorIds);
        
        Task<bool> AuthorExists(Guid authorId);
    }
}