using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SpeechBubble.Common.Requests;
using SpeechBubble.Common.Responses;
using SpeechBubble.Server.Models;
using SpeechBubble.Server.Services;

namespace SpeechBubble.Server.Controllers
{
    // TODO: Create result class to return as IActionResult value.
    [Route("api/1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly UserManager<User> _userManager;

        public AuthenticationController(IAuthenticationService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] SigninRequest request)
        {
            var password = System.Text.Encoding.Default.GetString(Convert.FromBase64String(request.Password));

            var loginResult = await _authService.SignInAsync(request.Email, password);

            if(loginResult.Succeeded == false)
                return new JsonResult(new AuthenticationResponse { success = false });

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            string jwtToken = await _authService.GenerateJWTToken(user.UserName, request.Email);

            return new JsonResult(new AuthenticationResponse { success = true, token = jwtToken });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] SignupRequest request)
        {
            var result = await _authService.Register(request.Email,request.Password,request.Username);

            if (result.Succeeded)
                return Ok();

            //string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))

            return BadRequest();
        }
    }
}
