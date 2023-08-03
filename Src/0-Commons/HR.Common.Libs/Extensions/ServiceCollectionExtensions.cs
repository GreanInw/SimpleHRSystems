using HR.Common.Libs.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace HR.Common.Libs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register(DI) <see cref="HttpResponseExceptionFilter"/> for exception handler.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static IServiceCollection RegisterExpcetionFilter(this IServiceCollection services)
        {
            services.AddControllers(option =>
            {
                option.Filters.Add(new HttpResponseExceptionFilter());
            }).ConfigureApiBehaviorOptions(option =>
            {
                option.SuppressConsumesConstraintForFormFileParameters = true;
                option.SuppressInferBindingSourcesForParameters = true;
                option.SuppressModelStateInvalidFilter = true;
                option.SuppressMapClientErrors = true;
                option.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";

                option.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);

                    // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                    return result;
                };
            });

            return services;
        }
    }
}
