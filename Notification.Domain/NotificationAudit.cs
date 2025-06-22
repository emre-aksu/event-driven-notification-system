namespace Notification.Domain
{
    public class NotificationAudit
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Recipient { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int RetryCount { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
