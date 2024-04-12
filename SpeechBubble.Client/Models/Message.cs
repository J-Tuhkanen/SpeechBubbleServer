using System;

namespace SpeechBubble.Client.Models
{
    // TODO: Use common project message class
    public class Message : ChatContent
    {
        public string Sender { get; set; }
        public string Timestamp { get; set; }

        public Message(string text, string sender, DateTime timestamp)
        {
            Text = text;
            Sender = sender;
            Timestamp = $"{timestamp.Hour}:{timestamp.Minute}";
            ContentType = ContentType.UserMessage;
        }
    }
}
