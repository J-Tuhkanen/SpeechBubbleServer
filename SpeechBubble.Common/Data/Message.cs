using System;

namespace SpeechBubble.Common.Data
{
    public class Message : EntityBase
    {
        public string Sender { get; set; }
        public string Content { get; set; }
    }
}
