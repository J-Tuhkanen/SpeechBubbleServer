using SpeechBubble.Client.ViewModels;
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

namespace SpeechBubble.Client.Views
{
    /// <summary>
    /// Interaction logic for ChatWindowView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();

            KeyDown += ChatView_KeyDown;
        }

        private void ChatView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ((ChatViewModel)DataContext).SendMessage();
            }
        }
    }
}
