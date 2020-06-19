using AutoMapper;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Courses;

namespace CourseLibrary.Persistence.Queries
{
    public class CourseViewProfile : Profile
    {
        public CourseViewProfile()
        {
            CreateMap<Course, CourseView>();
        }
    }
}