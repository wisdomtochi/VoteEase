using Serilog.Events;
using VoteEase.Application.Error;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Errors;

namespace VoteEase.Infrastructure.Error
{
    public class ErrorService : IErrorService
    {
        private readonly IGenericRepository<ErrorLog> errorGenericRepository;

        public ErrorService(IGenericRepository<ErrorLog> errorGenericRepository)
        {
            this.errorGenericRepository = errorGenericRepository;
        }

        public ErrorService()
        {
        }

        public void LogError(Exception ex)
        {
            SaveError(ex, LogEventLevel.Error);
        }

        public void SaveError(Exception ex, LogEventLevel logEventLevel)
        {
            ErrorLog newError = new()
            {
                Id = Guid.NewGuid(),
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
