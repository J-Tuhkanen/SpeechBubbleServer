using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SpeechBubble.Server.Models;
using SpeechBubble.Server.Data;

namespace SpeechBubble.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ApplicationDbContext _accountDbContext;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext accountDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountDbContext = accountDbContext;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<SignInResult> SignInAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return result;
        }

        public string GenerateJWTToken(User user)
        {
            var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, UnixTimeHelper.ToUnixTimeSeconds(DateTime.Now).ToString(), ClaimValueTypes.Integer64),
                        new Claim("UserId", user.Id.ToString())};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5ur04327154k3284pok5dh34851umf134+0658324905ds232103495vum98034u589d34lu583421k+u589sd234u53210dlk5u90231s4u59+3l4218590+d34f"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
              null,
              null,
              claims,
              expires: DateTime.UtcNow.AddHours(3),
              signingCredentials: signIn));
        }

        public async Task<IdentityResult> Register(string email, string password, string username)
        {
            var user = Activator.CreateInstance<User>();
            user.UserName = email;
            user.Email = email;
            user.DisplayName = username;

            return await _userManager.CreateAsync(user, password);
        }
    }

    public static class UnixTimeHelper
    {
        /// <summary>
        /// Converts a Unix time expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z to a DateTime value.
        /// </summary>
        public static DateTime FromUnixTimeSeconds(long seconds)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(seconds);
        }

        /// <summary>
        /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        public static long ToUnixTimeSeconds(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }
    }
}
