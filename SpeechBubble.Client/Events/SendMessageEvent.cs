using Prism.Events;
using SpeechBubble.Client.Models;

namespace SpeechBubble.Client.Events
{
    public class SendMessageEvent : PubSubEvent<SendMessageEventArgs>
    {

    }

    public class SendMessageEventArgs
    {
        public string Message { get; set; }
        public ContentType ContentType { get; set; }
    }
}
