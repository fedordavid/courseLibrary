using CourseLibrary.Application.Queries.Authors;
using CourseLibrary.Application.Queries.Courses;
using CourseLibrary.Persistence.DbContexts;
using CourseLibrary.Persistence.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Persistence
{
    public static class PersistenceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddViews();
            services.AddDbContext<CourseLibraryContext>(SetupLocalDb);
        }

        private static void AddViews(this IServiceCollection services)
        {
            services.AddScoped<ICourseViews, CourseLibraryQueryRepository>();
            services.AddScoped<IAuthorViews, CourseLibraryQueryRepository>();
        }

        private static void SetupLocalDb(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CourseLibraryDB;Trusted_Connection=True;");
        }
    }
}