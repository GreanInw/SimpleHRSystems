using HR.Common.DbContexts.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HR.Common.Services.BackgroundServices
{
    public class AutoMigrationService : IHostedService
    {
        private readonly ILogger<AutoMigrationService> _logger;
        private readonly IHRDbContext _dbContext;

        public AutoMigrationService(ILogger<AutoMigrationService> logger, IHRDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start auto migrate database.");
            try
            {
                await _dbContext.Database.MigrateAsync();
                _logger.LogInformation("End auto migrate database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Auto migrate database failure. {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop auto migrate database");
            return Task.CompletedTask;
        }
    }
}
