using Serilog.Events;

namespace VoteEase.Application.Error
{
    public interface IErrorService
    {
        void LogError(Exception ex);
        void SaveError(string message, LogEventLevel logEventLevel);
  