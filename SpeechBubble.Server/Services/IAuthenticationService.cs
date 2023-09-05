using Microsoft.AspNetCore.Identity;
using SpeechBubble.Server.Models;

namespace SpeechBubble.Server.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateJWTToken(string username, string email);
        Task<User> GetByIdAsync(string userId);
        Task<IdentityResult> Register(string email, string password, string username);
        Task<SignInResult> SignInAsync(string email, string password);
    }
}