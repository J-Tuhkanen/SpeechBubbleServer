using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Events
{
    public class ConnectedEvent : PubSubEvent<ConnectedEventArgs>
    {
    }

    public class ConnectedEventArgs
    {
        public string ServerUrl { get; set; }
        public string AccessToken { get; internal set; }
    }
}
