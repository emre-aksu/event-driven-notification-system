using Notification.Application.Services;
using Notification.Domain;

namespace Notification.Application.Implementation
{
    public class NotificationService : INotificationService
    {
        public async Task<bool> SendAsync(NotificationEvent notificationEvent)
        {
            try
            {
                Console.WriteLine($"Sending {notificationEvent.Type} to {notificationEvent.Recipient}");
                await Task.Delay(200); // Simulate async operation
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
