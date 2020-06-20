using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CourseLibrary.Host
{
    internal  static class HostConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddMvcOptions(ConfigureMvcOptions)
                .AddNewtonsoftJson(ConfigureJson)
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(ConfigureApiBehaviors);
        }

        private static void ConfigureApiBehaviors(ApiBehaviorOptions setupAction)
        {
            setupAction.InvalidModelStateResponseFactory = CreateInvalidModelStateResponse;
        }

        private static void ConfigureJson(MvcNewtonsoftJsonOptions setupAction)
        {
            setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            setupAction.SerializerSettings.Formatting = Formatting.Indented;
        }

        private static void ConfigureMvcOptions(MvcOptions setupAction)
        {
            setupAction.ReturnHttpNotAcceptable = true;
        }
        
        private static IActionResult CreateInvalidModelStateResponse(ActionContext context)
        {
            // create a problem details object
            var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
            var problemDetails =
                problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

            // add additional info not added by default
            problemDetails.Detail = "See the errors field for details.";
            problemDetails.Instance = context.HttpContext.Request.Path;

            // find out which status code to use
            var actionExecutingContext = context as ActionExecutingContext;

            // if there are modelstate errors & all keys were correctly
            // found/parsed we're dealing with validation errors
            if (context.ModelState.ErrorCount > 0 && actionExecutingContext?.ActionArguments.Count ==
                context.ActionDescriptor.Parameters.Count)
            {
                problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                problemDetails.Title = "One or more validation errors occurred.";

                return new UnprocessableEntityObjectResult(problemDetails)
                    {ContentTypes = {"application/problem+json"}};
            }

            // if one of the keys wasn't correctly found / couldn't be parsed
            // we're dealing with null/unparsable input
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "One or more errors on input occurred.";
            return new BadRequestObjectResult(problemDetails) {ContentTypes = {"application/problem+json"}};
        }
    }
}