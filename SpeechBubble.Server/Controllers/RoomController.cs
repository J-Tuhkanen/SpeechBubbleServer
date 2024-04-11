using Microsoft.AspNetCore.Mvc;
using SpeechBubble.Common.Responses;
using SpeechBubble.Server.Services;

namespace SpeechBubble.Server.Controllers
{
    [Route("api/1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom()
        {
            return new JsonResult(await _roomService.CreateRoom());
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomMessages(string roomId)
        {
            var room = await _roomService.GetRoom(new Guid(roomId));

            return room != null
                ? new JsonResult(new GetMessagesResponse { Messages = room.Messages })
                : NotFound();
        }
    }
}
