using HR.Common.Libs.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HR.Common.EFCores.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddDbContextHealthCheck(this IHealthChecksBuilder builder, IEnumerable<Type> dbContextTypes)
            => builder.AddTypeActivatedCheck<DbContextHealthCheck>("dbcontexts", HealthStatus.Healthy
                , tags: new[] { "dbcontexts" }, args: new object[] { dbContextTypes });
    }
}
