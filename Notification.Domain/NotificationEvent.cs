namespace Notification.Domain
{
    public class NotificationEvent
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
        public int RetryCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
