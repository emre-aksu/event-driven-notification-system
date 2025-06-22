using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface IPasswordResetService
    {
        Task<string> GenerateResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
    }
}
