namespace Notification.Domain.Models
{
    public class PasswordResetToken
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
