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
