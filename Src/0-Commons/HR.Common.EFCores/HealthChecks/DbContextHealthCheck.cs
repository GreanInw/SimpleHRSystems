using HR.Common.EFCores.DbContexts;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HR.Common.Libs.HealthChecks
{
    public class DbContextHealthCheck : IHealthCheck
    {
        private readonly IServiceProvider _provider;
        private readonly IEnumerable<Type> _types;

        public DbContextHealthCheck(IServiceProvider provider, IEnumerable<Type> types)
        {
            _provider = provider;
            _types = types;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var result = await ValidationCanConnect();
            var status = result.SelectMany(s => s.Value as Dictionary<string, bool>).Where(w => !w.Value).Any()
                ? HealthStatus.Unhealthy : HealthStatus.Healthy;

            return new HealthCheckResult(status, "Connect database result.", null, result);
        }

        private async Task<Dictionary<string, object>> ValidationCanConnect()
        {
            var values = new Dictionary<string, object>();
            foreach (var item in _types)
            {
                var dbContext = (IDbContext)_provider.GetService(item);
                bool canConnect = dbContext is null ? false : await dbContext.Database.CanConnectAsync();
                values.Add(item.Name, new Dictionary<string, bool> { { "CanConnect", canConnect } });
            }
            return values;
        }
    }
}