using Microsoft.AspNetCore.Mvc;
using Notification.Application.Dtos;
using Notification.Application.Interfaces;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(IPasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromBody] PasswordResetRequestDto dto)
        {
            var token = await _passwordResetService.GenerateResetTokenAsync(dto.Email);
            return Ok(new { Message = "Şifre sıfırlama linki üretildi.", Token = token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto dto)
        {
            var success = await _passwordResetService.ResetPasswordAsync(dto.Token, dto.NewPassword);
            if (!success)
                return BadRequest("Token geçersiz veya süresi dolmuş.");
            return Ok("Şifreniz güncellendi.");
        }
    }
}
