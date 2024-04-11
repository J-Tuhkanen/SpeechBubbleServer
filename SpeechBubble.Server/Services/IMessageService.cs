using SpeechBubble.Common.Data;

namespace SpeechBubble.Server.Services
{
    public interface IMessageService
    {
        Task SendMessageToRoom(Guid roomId, Message message);
    }
}