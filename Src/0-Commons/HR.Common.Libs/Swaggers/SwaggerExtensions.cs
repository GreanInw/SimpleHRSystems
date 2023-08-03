using HR.Common.Configurations.Settings;
using HR.Common.Libs.Extensions;
using HR.Common.Libs.Swaggers.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace HR.Common.Libs.Swaggers
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerInternal(this IServiceCollection services, params Assembly[] assemblies)
        {
            var serviceProvider = services.BuildServiceProvider();
            var httpContext = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
            var configuration = serviceProvider.GetService<IConfiguration>();

            var swaggerSettings = GetSwaggerSettings(configuration);
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<RemoveVersionFromParameterFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                options.DocInclusionPredicate((s, description) => description.ApplyDocInclusionPredicate(s, httpContext));

                options.AddSwaggerDocumentsInternal(swaggerSettings);
                options.AddIncludeXmlDocumentsInternal(assemblies);
            });

            return services;
        }

        public static WebApplication UseSwaggerInternal(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                return app;
            }

            var settings = GetSwaggerSettings(app.Configuration);
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.DocumentTitle = settings.DocumentTitle;

                foreach (var item in settings.Versions)
                {
                    options.SwaggerEndpoint($"/swagger/{item.Version}/swagger.json", item.Title);
                }
            });

            return app;
        }

        internal static string VersionFormatter(this string versionString)
        {
            // fieldCount format : major.minor.build.revision
            var version = versionString.Length == 1 ? new Version($"{versionString}.0") : new Version(versionString);

            if (version.Build > 0 && version.Minor > 0 && version.Major > 0 && version.Revision > 0)
                return $"v{version.ToString(4)}";
            if (version.Build > 0 && version.Minor > 0 && version.Major > 0)
                return $"v{version.ToString(3)}";
            if (version.Build > 0 && version.Minor > 0)
                return $"v{version.ToString(2)}";
            return $"v{version}";
        }

        private static SwaggerSettings GetSwaggerSettings(IConfiguration configuration)
            => configuration.GetValueBySection<SwaggerSettings>(nameof(SwaggerSettings));

        private static void AddSwaggerDocumentsInternal(this SwaggerGenOptions options, SwaggerSettings settings)
        {
            foreach (var item in settings.Versions)
            {
                options.SwaggerDoc(item.Version, new OpenApiInfo
                {
                    Version = item.Version,
                    Title = item.Title,
                    Description = item.Description
                });
            }
        }

        private static void AddIncludeXmlDocumentsInternal(this SwaggerGenOptions options, params Assembly[] assemblies)
        {
            if (assemblies is null) return;

            foreach (var item in assemblies)
            {
                var xmlFile = Path.Combine(AppContext.BaseDirectory, $"{item.GetName().Name}.xml");
                if (!File.Exists(xmlFile))
                {
                    throw new Exception($"Cannot find file paht : '{xmlFile}'.");
                }
                options.IncludeXmlComments(xmlFile);
            }
        }

        private static bool ApplyDocInclusionPredicate(this ApiDescription desc, string version, HttpContext httpContext)
        {
            if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

            var versions = methodInfo.ReflectedType?.GetCustomAttributes(true)
                .OfType<ApiVersionAttribute>().SelectMany(s => s.Versions);

            MapToApiVersionAttribute attr = null;
            if (desc.TryGetMethodInfo(out MethodInfo attrMethod) && attrMethod != null)
            {
                attr = attrMethod.GetCustomAttribute<MapToApiVersionAttribute>(true);
            }

            bool isMatchVersion;
            if (attr is null)
            {
                isMatchVersion = versions!.Any(v => v.ToString().VersionFormatter() == version);
            }
            else
            {
                var maps = attr.Versions;
                isMatchVersion = versions!.Any(v => v.ToString().VersionFormatter() == version)
                    && (!maps.Any() || maps.Any(v => v.ToString().VersionFormatter() == version));
            }

            if (isMatchVersion)
            {
                var filter = httpContext?.Request?.Query["filter"].ToString();
                var filterOut = httpContext?.Request?.Query["filterout"].ToString();

                if (!filterOut.IsEmpty())
                {
                    var filterOutArr = filterOut.Split(",");
                    if (filterOutArr.Any())
                        if (filterOutArr.Any(itemFilter =>
                            desc.ActionDescriptor.DisplayName.Contains(itemFilter, StringComparison.OrdinalIgnoreCase)))
                            return false;
                }

                if (!filter.IsEmpty())
                {
                    var filterListArr = filter.Split(",");
                    if (filterListArr.Any())
                        return filterListArr.Any(itemFilter =>
                            desc.ActionDescriptor.DisplayName.Contains(itemFilter, StringComparison.OrdinalIgnoreCase));
                }
            }

            return isMatchVersion;
        }
    }
}
