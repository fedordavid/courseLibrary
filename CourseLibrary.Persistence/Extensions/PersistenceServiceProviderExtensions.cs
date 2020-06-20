using System;
using System.Threading.Tasks;
using CourseLibrary.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CourseLibrary.Persistence.Extensions
{
    public static class PersistenceServiceProviderExtensions
    {
        public static async Task ConfigureDatabase(this IServiceProvider services, Func<DatabaseFacade, Task> cfg)
        {
            var context = services.GetService<CourseLibraryContext>();
            var logger = services.GetRequiredService<ILogger<CourseLibraryContext>>();

            try
            {
                await cfg(context.Database);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}