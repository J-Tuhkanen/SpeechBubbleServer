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

        public async Task SendMessageAllAsync(string roomId, string message)
        {
            User user = await _userManager.GetUserAsync(Context.User);

            await Clients.Group(roomId).SendAsync("ReceiveMessage", new Message { Content = message, Sender = user.UserName });

            Console.WriteLine($"{user.DisplayName}: {message}");
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            
            if(Context.GetHttpContext()?.GetRouteValue("roomId") is string roomId && string.IsNullOrWhiteSpace(roomId) == false) 
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                await base.OnConnectedAsync();
                
                Console.WriteLine($"{user.DisplayName}: Connected to room {roomId}");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            await base.OnDisconnectedAsync(exception);

            Console.WriteLine($"{user.DisplayName}: Disconnected");
        }
    }
}
