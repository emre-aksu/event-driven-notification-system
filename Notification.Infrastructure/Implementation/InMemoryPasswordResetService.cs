using Notification.Application.Interfaces;
using Notification.Domain.Models;
using System.Collections.Concurrent;

namespace Notification.Infrastructure.Services
{
    public class InMemoryPasswordResetService : IPasswordResetService
    {
        // Basit in-memory store
        private static ConcurrentDictionary<string, PasswordResetToken> _store = new();

        public Task<string> GenerateResetTokenAsync(string email)
        {
            var token = Guid.NewGuid().ToString();
            var model = new PasswordResetToken
            {
                Email = email,
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(15)
            };

            _store[token] = model;

            // Burada e-postaya token içeren link yollanabilir
            Console.WriteLine($"[TOKEN LINK] https://localhost:8080/reset-password?token={token}");

            return Task.FromResult(token);
        }

        public Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            if (!_store.TryGetValue(token, out var record))
                return Task.FromResult(false);

            if (record.Expiration < DateTime.UtcNow)
                return Task.FromResult(false);

            Console.WriteLine($"[ŞİFRE GÜNCELLENDİ] Email: {record.Email}, Yeni Şifre: {newPassword}");

            _store.TryRemove(token, out _);
            return Task.FromResult(true);
        }
    }
}
