using SpeechBubble.Common.Data;
using System.Collections.Generic;

namespace SpeechBubble.Common.Responses
{
    public class GetMessagesResponse
    {
        public IEnumerable<Message> Messages { get; set; }
    }
}
