using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Authors;
using CourseLibrary.Application.Queries.Core;
using CourseLibrary.Application.Queries.Courses;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Application
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICourseLibraryQueryService, CourseLibraryQueryService>();
            
            services.AddScoped<QueryBus, QueryBus>();

            services.AddScoped<IExecuteQuery<GetCoursesQuery, QueryResult<CourseView>>, GetCourses>();
            services.AddScoped<IExecuteQuery<GetCourseQuery, CourseView>, GetCourse>();
            services.AddScoped<IExecuteQuery<GetAuthorsQuery, AuthorView[]>, GetAuthors>();
        }
    }
}