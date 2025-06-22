namespace Notification.Application.Dtos
{
    public class PasswordResetDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
