using Microsoft.Extensions.Hosting;
using Notification.Application.Services;
using Notification.Domain;
using Notification.Infrastructure.Contract;
using Notification.Infrastructure.Logger;

public class NotificationWorker : BackgroundService
{
    private readonly INotificationQueue _queue;
    private readonly INotificationService _notificationService;
    private readonly IAuditLogger _auditLogger;

    public NotificationWorker(INotificationQueue queue, INotificationService notificationService, IAuditLogger auditLogger)
    {
        _queue = queue;
        _notificationService = notificationService;
        _auditLogger = auditLogger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var notification = _queue.Dequeue();
            if (notification is null)
            {
                await Task.Delay(1000, stoppingToken);
                continue;
            }

            bool success = false;
            string error = "";
            for (int i = 0; i < 3; i++)
            {
                success = await _notificationService.SendAsync(notification);
                if (success) break;
                notification.RetryCount++;
            }

            if (!success)
                error = "Failed after retries";

            await _auditLogger.LogAsync(new NotificationAudit
            {
                Type = notification.Type,
                Recipient = notification.Recipient,
                IsSuccess = success,
                ErrorMessage = error,
                RetryCount = notification.RetryCount,
            });
        }
    }
}
