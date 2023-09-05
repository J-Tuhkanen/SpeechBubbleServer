using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpeechBubble.Client.CustomControls
{
    public class ChatNotificationControl : Control
    {
        public string NotificationBody
        {
            get => (string)GetValue(NotificationBodyProperty);
            set => SetValue(NotificationBodyProperty, value);
        }

        public static readonly DependencyProperty NotificationBodyProperty = DependencyProperty.Register(nameof(NotificationBody), typeof(string), typeof(ChatNotificationControl));

        static ChatNotificationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChatNotificationControl), new FrameworkPropertyMetadata(typeof(ChatNotificationControl)));
        }
    }
}
