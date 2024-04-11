using Microsoft.AspNetCore.Identity;
using SpeechBubble.Server.Models;

namespace SpeechBubble.Server.Services
{
    public interface IAuthenticationService
    {
        string GenerateJWTToken(User user);
        Task<User> GetByIdAsync(string userId);
        Task<IdentityResult> Register(string email, string password, string username);
        Task<SignInResult> SignInAsync(string email, string password);
    }
}