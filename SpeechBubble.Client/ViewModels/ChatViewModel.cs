using Microsoft.AspNetCore.SignalR.Client;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.Models;
using SpeechBubble.Client.Services.Base;
using SpeechBubble.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpeechBubble.Client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private readonly IChatService _chatService;
        private bool isMessageTextBoxEnabled;
        private string _message = string.Empty;
        private ChatRoomViewModel _currentChatRoomViewModel = null!;

        public ChatRoomViewModel CurrentChatRoomViewModel 
        { 
            get => _currentChatRoomViewModel; 
            private set
            {
                _currentChatRoomViewModel = value;
                HasChatRoomOpen = value != null;
            }
        }
        public RoomListViewModel RoomListViewModel { get; init; }

        public bool HasChatRoomOpen 
        { 
            get => isMessageTextBoxEnabled; 
            private set => Set(ref isMessageTextBoxEnabled, value); 
        }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public ChatViewModel(IEventAggregator eventAggregator, IChatService chatService, RoomListViewModel roomListViewModel)
        {
            _eventAggregator = eventAggregator;
            _chatService = chatService;

            RoomListViewModel = roomListViewModel;

            _eventAggregator.GetEvent<OpenRoomEvent>().Subscribe(OnOpenRoomEvent);
            _eventAggregator.GetEvent<ConnectedEvent>().Subscribe(OnConnectedEvent);
        }

        private async void OnConnectedEvent(ConnectedEventArgs args)
        {
            await _chatService.JoinServerRoomsAsync(args);
        }

        private void OnOpenRoomEvent(OpenRoomEventArgs args)
        {
            CurrentChatRoomViewModel = _chatService.GetRoomById(args.RoomId);
            RaisePropertyChanged(nameof(CurrentChatRoomViewModel));
        }

        internal void SendMessage()
        {
            if(CurrentChatRoomViewModel != null)
                _chatService.SendMessage(CurrentChatRoomViewModel, Message);

            Message = string.Empty;
        }
    }
}
