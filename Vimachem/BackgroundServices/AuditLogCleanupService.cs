using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vimachem.Data;

namespace Vimachem.BackgroundServices
{
    public class AuditLogCleanupService: BackgroundService
    {
        private readonly ILogger<AuditLogCleanupService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromHours(24); // Run daily

        public AuditLogCleanupService(IServiceProvider serviceProvider, ILogger<AuditLogCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Audit Log Cleanup Service running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_interval, stoppingToken);

                try
                {
                    await CleanOldAuditLogs();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing cleanup.");
                }
            }
        }

        private async Task CleanOldAuditLogs()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var cutoffDate = DateTime.Now.AddDays(-20);
                var oldLogs = dbContext.AuditLog.Where(log => log.Timestamp < cutoffDate);

                dbContext.AuditLog.RemoveRange(oldLogs);
                int result = await dbContext.SaveChangesAsync();
                _logger.LogInformation($"Cleaned up {result} old audit log entries.");
            }
        }
    }

}
