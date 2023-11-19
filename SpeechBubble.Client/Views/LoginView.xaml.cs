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
        public LoginViewModel ViewModel { get; private set; }

        public LoginView()
        {
            InitializeComponent();
            
            KeyUp += OnKeyUp;
            DataContextChanged += LoginView_DataContextChanged;            
        }

        private void LoginView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {            
            if(DataContext != null)
            {
                ViewModel = (LoginViewModel)DataContext;
                ViewModel.OnClearForm += ViewModel_OnClearForm;
            }
        }

        private void ViewModel_OnClearForm(object sender)
        {
            email_box.Text = string.Empty;
            password_box.Password = string.Empty;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            if(key == Key.Enter)
                ViewModel.LoginCommand.Execute(null);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = ((PasswordBox)sender).SecurePassword;
        }
    }
}
