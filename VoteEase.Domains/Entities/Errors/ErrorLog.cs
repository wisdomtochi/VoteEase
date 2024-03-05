namespace VoteEase.Domain.Entities.Errors
{
    public class ErrorLog
    {
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public string SeverityLevel { get; set; }
    }
}
