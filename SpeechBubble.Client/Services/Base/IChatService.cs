using SpeechBubble.Client.Events;
using SpeechBubble.Client.Models;
using System;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Services.Base
{
    public interface IChatService
    {
        Task JoinServerRoomsAsync(ConnectedEventArgs args);
        ChatRoomViewModel GetRoomById(Guid id);
        void SendMessage(ChatRoomViewModel room, string message);
    }
}