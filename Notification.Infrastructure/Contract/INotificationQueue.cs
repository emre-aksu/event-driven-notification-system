using Notification.Domain;
using System.Collections.Concurrent;

namespace Notification.Infrastructure.Contract
{
    public interface INotificationQueue
    {
        void Enqueue(NotificationEvent notificationEvent);
        NotificationEvent? Dequeue();
    }

    public class InMemoryNotificationQueue : INotificationQueue
    {
        private readonly ConcurrentQueue<NotificationEvent> _queue = new();

        public void Enqueue(NotificationEvent notificationEvent)
            => _queue.Enqueue(notificationEvent);

        public NotificationEvent? Dequeue()
            => _queue.TryDequeue(out var notificationEvent) ? notificationEvent : null;
    }
}
