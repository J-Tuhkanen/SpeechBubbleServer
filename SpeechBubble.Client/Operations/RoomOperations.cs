using SpeechBubble.Common.Responses;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Operations
{
    public class RoomOperations : ApiOperationsBase
    {
        private string _roomUrl = _baseApiUrl + "Room";

        public async Task<GetMessagesResponse> GetMessagesAsync(string roomId)
        {
            var response = await GetAsync($"{_roomUrl}?roomId={roomId}", null);

            if (response.IsSuccessStatusCode == false)
            {
                new AuthenticationResponse { success = false };
            }

            var resonseMessage = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetMessagesResponse>(resonseMessage, new JsonSerializerOptions
            {
                 PropertyNameCaseInsensitive = true,
            });
        }
    }
}
