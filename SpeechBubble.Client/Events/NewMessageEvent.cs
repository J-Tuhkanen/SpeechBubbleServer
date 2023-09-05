using Prism.Events;
using SpeechBubble.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Events
{
    /// <summary>
    /// Event to notify the application for a new message.
    /// </summary>
    internal class NewMessageEvent : PubSubEvent<NewMessageEventArgs>
    {
    }

    internal class NewMessageEventArgs
    {
        public ChatContent Message { get; }

        public NewMessageEventArgs(ChatContent message)
        {
            Message = message;
        }
    }
}
