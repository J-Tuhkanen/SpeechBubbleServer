using Prism.Commands;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.Operations;
using SpeechBubble.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeechBubble.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // TODO: Replace with configuration later.
        private AuthorizationOperations _authorizationOperations = new AuthorizationOperations();
        private readonly IEventAggregator _eventAggregator;
        private const string _serverAddress = "https://localhost:7093/chathub";
        private string _email = "janne.tuhkanen@hotmail.com";
        private SecureString _password;
        private bool _isConneting;

        public string Email 
        { 
            get => _email; 
            set => Set(ref _email, value); 
        }

        public SecureString Password 
        { 
            get => _password; 
            set => Set(ref _password, value); 
        }

        public bool IsConnecting { get => _isConneting; set => Set(ref _isConneting, value); }

        public ICommand LoginCommand { get; }
        public ChatViewModel ChatViewModel { get; }

        public LoginViewModel(IEventAggregator eventAggregator) 
        {
            _eventAggregator = eventAggregator;

            LoginCommand = new DelegateCommand(OnLoginCommandExecute);
        }

        private async void OnLoginCommandExecute()
        {
            try
            {
                IsConnecting = true;

                var loginResponse = await _authorizationOperations.LoginAsync(Email, SecureStringToBase64(Password));

                if(loginResponse.success)
                    _eventAggregator.GetEvent<ConnectedEvent>().Publish(new ConnectedEventArgs (loginResponse.rooms, _serverAddress, loginResponse.token));
                
                else
                {
                    Email = string.Empty;
                    Password = new SecureString();

                    throw new Exception("Error authentication to chat servers.");
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                IsConnecting = false;
            }
        }

        private string SecureStringToBase64(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(value))));
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
