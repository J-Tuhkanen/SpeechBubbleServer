
using Prism.Commands;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.ViewModels.Base;
using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace SpeechBubble.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private ViewModelBase _contentViewModel;

        public LoginViewModel LoginViewModel { get; }
        public ChatViewModel ChatViewModel { get; }

        public ViewModelBase ContentViewModel 
        { 
            get => _contentViewModel; 
            set => Set(ref _contentViewModel, value); 
        }

        internal void OnClosing(CancelEventArgs cancelEventArgs)
        {

        }

        public MainWindowViewModel(IEventAggregator eventAggregator, LoginViewModel loginViewModel, ChatViewModel chatWindowViewModel)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ConnectedEvent>().Subscribe(OnConnectedEvent);
            _eventAggregator.GetEvent<DisconnectEvent>().Subscribe(OnDisconnectEvent);

            LoginViewModel = loginViewModel;
            ChatViewModel = chatWindowViewModel;

            ContentViewModel = LoginViewModel;
        }

        private void OnDisconnectEvent()
        {
            ContentViewModel = LoginViewModel;
        }

        private void OnConnectedEvent(ConnectedEventArgs args)
        {
            ContentViewModel = ChatViewModel;
        }
    }
}
