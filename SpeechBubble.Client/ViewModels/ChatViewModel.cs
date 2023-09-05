using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.Models;
using SpeechBubble.Client.Operations;
using SpeechBubble.Client.ViewModels.Base;
using SpeechBubble.Common.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpeechBubble.Client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        private ObservableCollection<ChatContent> _chatMessages = new();
        private string jwt;
        private HubConnection _connection;
        private string _message;

        public IEnumerable<ChatContent> ChatMessages => _chatMessages;

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public ICommand DisconnectCommand { get; }

        public ChatViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewMessageEvent>().Subscribe(OnNewMessageEvent);
            _eventAggregator.GetEvent<ConnectedEvent>().Subscribe(OnConnectEvent);
            _eventAggregator.GetEvent<SendMessageEvent>().Subscribe(OnSendMessageEvent);

            DisconnectCommand = new DelegateCommand(OnDisconnectCommand);
        }

        private async void OnSendMessageEvent(SendMessageEventArgs args)
        {
            await _connection?.InvokeAsync("SendMessageAllAsync", args.Message);
        }

        private async void OnConnectEvent(ConnectedEventArgs args)
        {
            _connection?.DisposeAsync();

            _connection = new HubConnectionBuilder()
                .WithUrl(args.ServerUrl, configureHttpConnection: options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(args.AccessToken);
                })
                .WithAutomaticReconnect()
                .Build();

            await _connection.StartAsync();
            //_connection.Reconnecting += _connection_Reconnecting;
            //_connection.Reconnected += _connection_Reconnected;
            //_connection.Closed += _connection_Closed;

            _connection.On<Common.Data.Message>("ReceiveMessage", async (e) =>
            {
                await Helpers.DispatcherHelper.RunAsync(() => 
                { 
                    _chatMessages.Add(new Models.Message(e.Content, e.Sender, DateTime.Now));
                });
            });
        }

        internal void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(Message) || string.IsNullOrEmpty(Message))
                return;

            _eventAggregator.GetEvent<SendMessageEvent>().Publish(new SendMessageEventArgs { ContentType = ContentType.UserMessage, Message = Message });

            Message = null;
        }

        private void OnNewMessageEvent(NewMessageEventArgs obj)
        {
            _chatMessages.Add(obj.Message);

            RaisePropertyChanged(nameof(ChatMessages));
        }

        private async void OnDisconnectCommand()
        {
            _eventAggregator.GetEvent<DisconnectEvent>().Publish();
            jwt = null;
            await _connection.DisposeAsync();

            _chatMessages.Clear();
            _chatMessages.Add(new Notification("Disconnected")
            {
                ContentType = ContentType.ServerNotification
            });
        }
    }
}
