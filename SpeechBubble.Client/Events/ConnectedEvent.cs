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
        public IEnumerable<string> RoomsIdCollection { get; set; }
        public string ServerUrl { get; set; } = null!;
        public string AccessToken { get; internal set; } = null!;

        public ConnectedEventArgs(IEnumerable<string> rooms, string serverUrl, string accessToken)
        {
            RoomsIdCollection = rooms;
            ServerUrl = serverUrl;
            AccessToken = accessToken;
        }
    }
}
