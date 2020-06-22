using CourseLibrary.Application.Commands;
using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Authors;
using CourseLibrary.Application.Queries.Core;
using CourseLibrary.Application.Queries.Courses;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Application
{
    public static class ApplicationConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddCommands();
            services.AddQueries();
        }

        private static void AddCommands(this IServiceCollection services)
        {
            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();
        }

        private static void AddQueries(this IServiceCollection services)
        {
            services.AddScoped<ICourseLibraryQueryService, CourseLibraryQueryService>();
            
            services.AddScoped<QueryBus, QueryBus>();

            services.AddScoped<IExecuteQuery<GetCoursesQuery, QueryResult<CourseView>>, GetCourses>();
            services.AddScoped<IExecuteQuery<GetAllCoursesQuery, QueryResult<CourseView>>, GetAllCourses>();
            services.AddScoped<IExecuteQuery<GetCourseQuery, CourseView>, GetCourse>();
            services.AddScoped<IExecuteQuery<GetAuthorsQuery, AuthorView[]>, GetAuthors>();
        }
    }
}