using Microsoft.AspNetCore.Mvc;
using Notification.Domain;
using Notification.Infrastructure.Contract;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationQueue _queue;

        public NotificationsController(INotificationQueue queue)
        {
            _queue = queue;
        }

        [HttpPost("send")]
        public IActionResult Send([FromBody] NotificationEvent notification)
        {
            _queue.Enqueue(notification);
            return Accepted();
        }

    }

}
