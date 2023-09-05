namespace SpeechBubble.Client.Models
{
    public class ChatContent
    {
        public string Text { get; set; }
        public ContentType ContentType { get; set; }
    }

    public enum ContentType
    {
        ServerNotification,
        UserMessage
    }
}