using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
