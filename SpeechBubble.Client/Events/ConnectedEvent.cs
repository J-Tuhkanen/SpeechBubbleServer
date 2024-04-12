using Prism.Events;
using System;
using System.Collections.Generic;

namespace SpeechBubble.Client.Events
{
    public class ConnectedEvent : PubSubEvent<ConnectedEventArgs>
    {
    }

    public class ConnectedEventArgs
    {
        public IEnumerable<Guid> RoomsIdCollection { get; set; }
        public string ServerUrl { get; set; } = null!;
        public string AccessToken { get; internal set; } = null!;

        public ConnectedEventArgs(IEnumerable<Guid> rooms, string serverUrl, string accessToken)
        {
            RoomsIdCollection = rooms;
            ServerUrl = serverUrl;
            AccessToken = accessToken;
        }
    }
}
