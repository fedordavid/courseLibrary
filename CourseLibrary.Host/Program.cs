using System;
using System.Threading.Tasks;
using CourseLibrary.Persistence;
using CourseLibrary.Persistence.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using HostBuilder = Microsoft.Extensions.Hosting.Host;

namespace CourseLibrary.Host
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await RunMigrations(host.Services);

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return HostBuilder
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(UseStartup);

            static void UseStartup(IWebHostBuilder b) => b.UseStartup<Startup>();
        }

        private static async Task RunMigrations(IServiceProvider rootServiceProvider)
        {
            using var scope = rootServiceProvider.CreateScope();
            
            await scope.ServiceProvider.ConfigureDatabase(async db =>
            {
                // db.EnsureDeleted();
                await db.MigrateAsync();
            });
        }
    }
}