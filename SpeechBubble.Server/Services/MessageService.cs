using Microsoft.EntityFrameworkCore;
using SpeechBubble.Common.Data;
using SpeechBubble.Server.Data;

namespace SpeechBubble.Server.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MessageService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SendMessageToRoom(Guid roomId, Message message)
        {
            var room = await _applicationDbContext.Room.FirstAsync(r => r.Id == roomId);

            room.Messages.Add(message);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
