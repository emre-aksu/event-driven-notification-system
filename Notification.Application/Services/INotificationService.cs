using Notification.Domain;

namespace Notification.Application.Services
{
    public interface INotificationService
    {
        Task<bool> SendAsync(NotificationEvent notification);
        
    }
}
