using SpeechBubble.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();

            KeyUp += OnKeyUp;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            if(key == Key.Enter)
                ((LoginViewModel)DataContext).LoginCommand.Execute(null);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginViewModel)DataContext).Password = ((PasswordBox)sender).SecurePassword;
        }
    }
}
