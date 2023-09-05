using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Models
{
    public class Notification : ChatContent
    {
        public Notification(string notificationText)
        {
            Text = notificationText;
            ContentType = ContentType.ServerNotification;
        }
    }
}
