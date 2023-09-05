using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SpeechBubble.Common.Data;
using SpeechBubble.Server.Models;

namespace SpeechBubble.Server.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;

        public ChatHub(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }

        public async Task SendMessageAllAsync(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            await Clients.All.SendAsync("ReceiveMessage", new Message { Sender = user.DisplayName, Content = message });

            Console.WriteLine($"{user.DisplayName}: {message}");
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);

            Console.WriteLine($"{user.DisplayName}: Connected");
            //await Clients.All.SendAsync("ReceiveMessage", new Message { Sender });
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            Console.WriteLine($"{user.DisplayName}: Disconnected");
        }
    }
}
