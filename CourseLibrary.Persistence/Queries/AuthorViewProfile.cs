using AutoMapper;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Authors;

namespace CourseLibrary.Persistence.Queries
{
    public class AuthorViewProfile : Profile
    {
        public AuthorViewProfile()
        {
            CreateMap<Author, AuthorView>()
                .ForMember(a => a.Name, e => e.MapFrom(a => a.FirstName + " " + a.LastName));
        }
    }
}