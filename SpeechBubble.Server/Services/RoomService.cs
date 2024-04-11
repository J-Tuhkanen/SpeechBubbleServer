using Microsoft.EntityFrameworkCore;
using SpeechBubble.Common.Data;
using SpeechBubble.Server.Data;

namespace SpeechBubble.Server.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoomService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Room>> GetRooms()
            => await _applicationDbContext.Room.ToListAsync();

        public async Task<Guid> CreateRoom()
        {
            var room = new Room();

            await _applicationDbContext.Room.AddAsync(room);
            await _applicationDbContext.SaveChangesAsync();

            return room.Id;
        }

        public async Task<Room?> GetRoom(Guid roomId)
        {
            var room = await _applicationDbContext.Room
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            return room;
        }
    }
}
