using SpeechBubble.Common.Requests;
using SpeechBubble.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
