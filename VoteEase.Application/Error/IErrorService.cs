using Serilog.Events;

namespace VoteEase.Application.Error
{
    public interface IErrorService
    {
        void LogError(Exception ex);
        void SaveError(Exception ex, LogEventLevel logEventLevel);
        Task CleanupLogs();
    }
}
