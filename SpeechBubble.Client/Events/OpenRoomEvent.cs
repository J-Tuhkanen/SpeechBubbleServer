using Prism.Events;
using System;

namespace SpeechBubble.Client.Events
{
    public class OpenRoomEvent : PubSubEvent<OpenRoomEventArgs>
    {

    }

    public class OpenRoomEventArgs
    {
        public Guid RoomId { get; set; }
    }
}
