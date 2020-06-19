using System;
using CourseLibrary.Application.Entities;

namespace CourseLibrary.Application.Commands
{
    public interface ICourseLibraryRepository
    {    
        void AddCourse(Guid authorId, Course course);
        
        void UpdateCourse(Course course);
        
        void DeleteCourse(Course course);
        
        void AddAuthor(Author author);
        
        void DeleteAuthor(Author author);
        
        void UpdateAuthor(Author author);
        
        bool Save();
    }
}
