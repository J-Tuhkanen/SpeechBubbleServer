using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Models
{
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
