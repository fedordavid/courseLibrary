using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Application.Entities;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, Models.CourseDto>();
            CreateMap<Models.CourseForCreationDto, Course>();
            CreateMap<Models.CourseForUpdateDto, Course>();
            CreateMap<Course, Models.CourseForUpdateDto>();
        }
    }
}
