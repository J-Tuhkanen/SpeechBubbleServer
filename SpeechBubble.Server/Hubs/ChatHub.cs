using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SpeechBubble.Common.Data;
using SpeechBubble.Server.Data;
using SpeechBubble.Server.Models;

namespace SpeechBubble.Server.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ChatHub(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task SendMessageAllAsync(string roomId, string message)
        {
            User user = await _userManager.GetUserAsync(Context.User);

            var room = await _dbContext.Room.FirstAsync(r => r.Id == new Guid(roomId));
            var messageEntity = new Message { Content = message, Sender = user.UserName };

            room.Messages.Add(messageEntity);
            await _dbContext.SaveChangesAsync();

            await Clients.Group(roomId).SendAsync("ReceiveMessage", messageEntity);

            Console.WriteLine($"{user.DisplayName}: {message}");
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            
            if(Context.GetHttpContext()?.GetRouteValue("roomId") is string roomId) 
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
