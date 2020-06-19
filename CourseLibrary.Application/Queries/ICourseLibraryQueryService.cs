using System;
using System.Collections.Generic;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.ResourceParameters;

namespace CourseLibrary.Application.Queries
{
    public interface ICourseLibraryQueryService
    {
        Course[] GetCourses(Guid authorId);
        Course GetCourse(GetCourseQuery query);
        Author[] GetAuthors(GetAuthorsQuery getAuthorsQuery);
        Author GetAuthor(Guid authorId);
        Author[] GetAuthors(IEnumerable<Guid> authorIds);
        bool AuthorExists(Guid authorId);
    }
}