using SpeechBubble.Client.Events;
using SpeechBubble.Client.Models;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Services.Base
{
    public interface IChatService
    {
        Task JoinServerRoomsAsync(ConnectedEventArgs args);
        ChatRoomViewModel GetRoomById(string id);
        void SendMessage(ChatRoomViewModel room, string message);
    }
}