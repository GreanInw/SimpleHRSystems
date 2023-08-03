using HR.Common.Configurations.Settings;
using HR.Common.Libs.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HR.Common.Libs.HealthChecks
{
    public class ReleaseVersionHealthCheck : IHealthCheck
    {
        private readonly ReleaseVersionSettings _settings;

        public ReleaseVersionHealthCheck(IConfiguration configuration)
            => _settings = configuration.GetValueBySection<ReleaseVersionSettings>(nameof(ReleaseVersionSettings));

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
            => await Task.Run(() =>
            {
                bool isMissingConfig = _settings.IsNullable() ? true : _settings.VersionInfo.IsEmpty();
                string description = "Release version health result.";
                var status = !isMissingConfig ? HealthStatus.Healthy : HealthStatus.Unhealthy;
                var values = new Dictionary<string, object>
                {
                    { nameof(_settings.VersionInfo), _settings.VersionInfo }
                };
                return new HealthCheckResult(status, description, null, values);
            });
    }
}