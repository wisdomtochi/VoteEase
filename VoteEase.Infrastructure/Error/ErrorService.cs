using Serilog;
using Serilog.Events;
using VG.Serilog.Sinks.EntityFrameworkCore;
using VoteEase.Application.Error;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Errors;

namespace VoteEase.Infrastructure.Error
{
    public class ErrorService : IErrorService
    {
        private readonly Serilog.Core.Logger logger;
        private readonly IGenericRepository<ErrorLog> errorGenericRepository;

        public ErrorService(IGenericRepository<ErrorLog> errorGenericRepository,
                            Func<LogDbContext> dbContext)
        {
            errorGenericRepository = errorGenericRepository;

            logger = new LoggerConfiguration()
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                    .WriteTo.EntityFrameworkSink(dbContext)).CreateLogger();
        }

        public ErrorService()
        {
        }

        public void LogError(Exception ex)
        {
            logger.Error(ex, ex.Message);
            SaveError(ex, LogEventLevel.Error);
        }

        public void SaveError(Exception ex, LogEventLevel logEventLevel)
        {
            ErrorLog newError = new()
            {
                ErrorMessage = ex.Message,
                Timestamp = DateTime.UtcNow,
                SeverityLevel = logEventLevel.ToString()
            };

            errorGenericRepository.Create(newError);
            errorGenericRepository.SaveChanges();
        }

        public async Task CleanupLogs()
        {
            var retentionPeriod = TimeSpan.FromDays(30);

            var cutOffDate = DateTime.UtcNow.Subtract(retentionPeriod);

            var storeLogsToDelete = await errorGenericRepository.ReadAll();

            var logsToDelete = storeLogsToDelete.Where(log => log.Timestamp < cutOffDate);

            errorGenericRepository.RemoveRange(logsToDelete);
            await errorGenericRepository.SaveChanges();
        }
    }

}
