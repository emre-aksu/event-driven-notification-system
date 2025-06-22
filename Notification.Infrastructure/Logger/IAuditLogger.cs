using Notification.Domain;

namespace Notification.Infrastructure.Logger
{
    public interface IAuditLogger
    {
        Task LogAsync(NotificationAudit audit);
    }
    public class InMemoryAuditLogger : IAuditLogger
    {
        private readonly List<NotificationAudit> _logs = new();

        public Task LogAsync(NotificationAudit audit)
        {
            _logs.Add(audit);
            Console.WriteLine($"Audit Log: {audit.Type} to {audit.Recipient}, Success: {audit.IsSuccess}");
            return Task.CompletedTask;
        }
    }
}
