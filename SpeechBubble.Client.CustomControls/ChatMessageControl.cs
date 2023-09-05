using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpeechBubble.Client.CustomControls
{

    public class ChatMessageControl : Control
    {
        public string MessageTimestamp
        {
            get => (string)GetValue(MessageTimestampProperty);
            set => SetValue(MessageTimestampProperty, value);
        }

        public string MessageSender
        {
            get { return (string)GetValue(MessageSenderProperty); }
            set { SetValue(MessageSenderProperty, value); }
        }

        public string MessageBody
        {
            get => (string)GetValue(MessageBodyProperty);
            set => SetValue(MessageBodyProperty, value);
        }

        public static readonly DependencyProperty MessageTimestampProperty = DependencyProperty.Register(nameof(MessageTimestamp), typeof(string), typeof(ChatMessageControl));
        public static readonly DependencyProperty MessageSenderProperty = DependencyProperty.Register(nameof(MessageSender), typeof(string), typeof(ChatMessageControl));
        public static readonly DependencyProperty MessageBodyProperty = DependencyProperty.Register(nameof(MessageBody), typeof(string), typeof(ChatMessageControl));

        static ChatMessageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChatMessageControl), new FrameworkPropertyMetadata(typeof(ChatMessageControl)));
        }
    }
}
