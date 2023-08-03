using HR.Common.Libs.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Common.Libs.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddReleaseVersionHealthCheck(this IHealthChecksBuilder builder)
            => builder.AddCheck<ReleaseVersionHealthCheck>("release_version", tags: new[] { "release_version" });

        public static void UseHealthChecks(this IEndpointRouteBuilder builder)
            => builder.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponse.WriteResponse
            });

        public static void UseHealthChecks(this WebApplication app)
            => app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponse.WriteResponse
            });
    }
}