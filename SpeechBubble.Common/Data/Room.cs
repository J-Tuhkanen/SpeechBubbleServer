using System.Collections.Generic;

namespace SpeechBubble.Common.Data
{
    public class Room : EntityBase
    {
        public virtual List<Message> Messages { get; } = new List<Message>();
    }
}
