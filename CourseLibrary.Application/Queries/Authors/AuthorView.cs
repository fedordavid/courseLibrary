using System;
using System.Linq;

namespace CourseLibrary.Application.Queries.Authors
{
    public class AuthorView
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } 
        
        public DateTime BirthDate { get; set; }
        
        public string MainCategory { get; set; }
    }

    public interface IAuthorViews
    {
        IQueryable<AuthorView> Authors { get; }
    }
}