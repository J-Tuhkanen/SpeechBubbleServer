using SpeechBubble.Common.Data;

namespace SpeechBubble.Server.Services
{
    public interface IRoomService
    {
        Task<Guid> CreateRoom();
        Task<IEnumerable<Room>> GetRooms();
        Task<Room> GetRoom(Guid roomId);
    }
}