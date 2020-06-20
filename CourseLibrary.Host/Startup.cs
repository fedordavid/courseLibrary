using System;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Application;
using CourseLibrary.Persistence;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourseLibrary.Host
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            ApplicationConfiguration.Configure(services);
            HostConfiguration.Configure(services);
            PersistenceConfiguration.Configure(services);
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ExceptionHandler);
            }

            app.UseRouting();
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ExceptionHandler(IApplicationBuilder appBuilder)
        {
            appBuilder.Run(HandleExceptions);
        }

        private static async Task HandleExceptions(HttpContext context)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
        }

    }
}